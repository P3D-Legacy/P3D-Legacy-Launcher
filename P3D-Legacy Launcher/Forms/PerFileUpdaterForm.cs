using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using DamienG.Security.Cryptography;

using Octokit;

using P3D.Legacy.Launcher.Controls;
using P3D.Legacy.Launcher.Data;
using P3D.Legacy.Launcher.Extensions;
using P3D.Legacy.Launcher.Services;
using P3D.Legacy.Launcher.Storage.Files;
using P3D.Legacy.Launcher.Storage.Folders;
using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Forms
{
    internal partial class PerFileUpdaterForm : LocalizableForm
    {
        private WebClient Downloader { get; set; }
        private bool Cancelled { get; set; }

        private ReleaseAsset OldUpdateInfoAsset { get; }
        private ReleaseAsset NewUpdateInfoAsset { get; }

        private TempFile OldUpdateInfoFile => new TempFile(OldUpdateInfoAsset.Name);
        private TempFile NewUpdateInfoFile => new TempFile(NewUpdateInfoAsset.Name);

        private IFolder UpdatedFolder { get; }

        private Uri DLUri { get; }

        public PerFileUpdaterForm(ReleaseAsset oldUpdateInfoAsset, ReleaseAsset newUpdateInfoAsset, IFolder updatedFolder, Uri dlUri)
        {
            OldUpdateInfoAsset = oldUpdateInfoAsset;
            NewUpdateInfoAsset = newUpdateInfoAsset;
            UpdatedFolder = updatedFolder;
            DLUri = dlUri;

            InitializeComponent();
        }

        private void CustomUpdaterForm_Shown(object sender, EventArgs e)
        {
            DownloadUpdateInfoFiles();

            if (Cancelled) return;

            CheckOldFiles();

            if (Cancelled) return;

            var list = StartUpdate();

            if (Cancelled) return;

            if (list.Any())
            {
                UpdateFiles(list);
                if (Cancelled) return;
                UpdatedMessage();
            }
            else
                NoUpdateNeededMessage();


            Close();
        }

        private async void DownloadUpdateInfoFiles()
        {
            OldUpdateInfoFile.Delete();
            NewUpdateInfoFile.Delete();

            try
            {
                using (Downloader = new WebClient())
                {
                    Downloader.DownloadProgressChanged += (sender, args) => PercentageProgressBar.SafeInvoke(() => PercentageProgressBar.Value = (int) args.BytesReceived);

                    PercentageProgressBar.SafeInvoke(() => PercentageProgressBar.Maximum = OldUpdateInfoAsset.Size);
                    await Downloader.DownloadFileTaskAsync(new Uri(OldUpdateInfoAsset.BrowserDownloadUrl), OldUpdateInfoFile.Path);

                    PercentageProgressBar.SafeInvoke(() => PercentageProgressBar.Maximum = NewUpdateInfoAsset.Size);
                    await Downloader.DownloadFileTaskAsync(new Uri(NewUpdateInfoAsset.BrowserDownloadUrl), NewUpdateInfoFile.Path);
                }
            }
            catch (WebException) { DownloadErrorMessage(); return; }
        }

        private void CheckOldFiles()
        {
            var oldUpdateInfo = UpdateInfoYaml.Deserialize(OldUpdateInfoFile.ReadAllText());
            var newUpdateInfo = UpdateInfoYaml.Deserialize(NewUpdateInfoFile.ReadAllText());


            var toBeDeleted = new List<IFile>();
            var crc32 = new Crc32();
            var sha1 = new SHA1Managed();
            PercentageProgressBar.SafeInvoke(delegate { PercentageProgressBar.Maximum = oldUpdateInfo.Count(); PercentageProgressBar.Step = 1; PercentageProgressBar.Value = 0; });
            Parallel.ForEach(oldUpdateInfo, (oldUpdateFileEntry, state) =>
            {
                var newUpdateFileEntry  = newUpdateInfo.FirstOrDefault(newUpdateInfoFile => newUpdateInfoFile.AbsoluteFilePath == oldUpdateFileEntry.AbsoluteFilePath);

                if (Cancelled) state.Stop();

                PercentageProgressBar.SafeInvoke(() => PercentageProgressBar.PerformStep());

                if (UpdatedFolder.CheckExists(oldUpdateFileEntry.AbsoluteFilePath) != ExistenceCheckResult.FileExists)
                    state.Break();

                var folder = UpdatedFolder;
                var folders = oldUpdateFileEntry.AbsoluteFilePath.Split('\\');
                foreach (var folderName in folders.Reverse().Skip(1).Reverse())
                    folder = folder.GetFolder(folderName);

                var file = UpdatedFolder.GetFile(folders.Reverse().Take(1).ToArray()[0]);
                using (var fs = file.Open(PCLExt.FileStorage.FileAccess.Read))
                {
                    var crc32Hash = string.Empty;
                    var sha1Hash = string.Empty;
                    crc32Hash = crc32.ComputeHash(fs).Aggregate(crc32Hash, (current, b) => current + b.ToString("x2").ToLower());
                    if (crc32Hash == oldUpdateFileEntry.CRC32 && crc32Hash == newUpdateFileEntry.CRC32)
                    {
                        sha1Hash = sha1.ComputeHash(fs).Aggregate(sha1Hash, (current, b) => current + b.ToString("x2").ToLower());
                        if (sha1Hash == oldUpdateFileEntry.SHA1 && sha1Hash == newUpdateFileEntry.SHA1)
                            return; // -- Everything matches, do nothing.
                        else
                        {
                            toBeDeleted.Add(file);
                            return; // -- Should be deleted;
                        }
                    }
                    else
                    {
                        toBeDeleted.Add(file);
                        return; // -- Should be deleted;
                    }
                }
            });
            foreach (var file in toBeDeleted)
                file.Delete();
        }

        private List<UpdateFileEntryYaml> StartUpdate()
        {
            if (Cancelled) return new List<UpdateFileEntryYaml>();

            Label_ProgressBar1.SafeInvoke(() => Label_ProgressBar1.Text = Label_ProgressBar2.Text);

            var newUpdateInfo = UpdateInfoYaml.Deserialize(NewUpdateInfoFile.ReadAllText());

            var crc32 = new Crc32();
            var sha1 = new SHA1Managed();
            var notValidFileEntries = new List<UpdateFileEntryYaml>();
            PercentageProgressBar.SafeInvoke(delegate { PercentageProgressBar.Maximum = newUpdateInfo.Count(); PercentageProgressBar.Step = 1; PercentageProgressBar.Value = 0; });
            Parallel.ForEach(newUpdateInfo, (updateFileEntry, state) =>
            {
                if(Cancelled) state.Stop();

                PercentageProgressBar.SafeInvoke(() => PercentageProgressBar.PerformStep());

                if (UpdatedFolder.CheckExists(updateFileEntry.AbsoluteFilePath) != ExistenceCheckResult.FileExists)
                {
                    notValidFileEntries.Add(updateFileEntry);
                    return;
                }
                using (var fs = UpdatedFolder.GetFile(updateFileEntry.AbsoluteFilePath).Open(PCLExt.FileStorage.FileAccess.Read))
                {
                    var crc32Hash = string.Empty;
                    var sha1Hash = string.Empty;
                    crc32Hash = crc32.ComputeHash(fs).Aggregate(crc32Hash, (current, b) => current + b.ToString("x2").ToLower());
                    if (crc32Hash == updateFileEntry.CRC32)
                    {
                        sha1Hash = sha1.ComputeHash(fs).Aggregate(sha1Hash, (current, b) => current + b.ToString("x2").ToLower());
                        if (sha1Hash == updateFileEntry.SHA1)
                            return;
                        else
                        {
                            notValidFileEntries.Add(updateFileEntry);
                            return;
                        }
                    }
                    else
                    {
                        notValidFileEntries.Add(updateFileEntry);
                        return;
                    }
                }
            });

            return notValidFileEntries;
        }
        private void UpdateFiles(List<UpdateFileEntryYaml> updateFileEntries)
        {
            if (Cancelled) return;

            Label_ProgressBar1.SafeInvoke(() => Label_ProgressBar1.Text = Label_ProgressBar3.Text);

            try
            {
                PercentageProgressBar.SafeInvoke(delegate { PercentageProgressBar.Maximum = updateFileEntries.Count; PercentageProgressBar.Step = 1; PercentageProgressBar.Value = 0; });
                Parallel.ForEach(updateFileEntries, (updateFileEntry, state) =>
                {
                    if (Cancelled) state.Stop();

                    var dlUri = new Uri(DLUri, updateFileEntry.AbsoluteFilePath);
                    var tempFile = new TempFile(updateFileEntry.AbsoluteFilePath);
                    using (var downloader = new WebClient())
                    {
                        downloader.DownloadFile(dlUri, tempFile.Path);
                        PercentageProgressBar.SafeInvoke(() => PercentageProgressBar.PerformStep());
                    }
                });
            }
            catch (UriFormatException)          { DownloadErrorMessage(); return; }
            catch (UnauthorizedAccessException) { DownloadErrorMessage(); return; }
            catch (IOException)                 { DownloadErrorMessage(); return; }
            catch (WebException)                { DownloadErrorMessage(); return; }
            
            ReplaceFiles(updateFileEntries);
        }
        private void ReplaceFiles(List<UpdateFileEntryYaml> updateFileEntries)
        {
            if (Cancelled) return;

            try
            {
                Parallel.ForEach(updateFileEntries, (updateFileEntry, state) =>
                {
                    if (Cancelled) state.Break();

                    var tempFilePath = new TempFile(updateFileEntry.AbsoluteFilePath);
                    tempFilePath.Move(PortablePath.Combine(UpdatedFolder.Path, updateFileEntry.AbsoluteFilePath));
                    tempFilePath.Delete();
                });
            }
            catch (UnauthorizedAccessException) { FileReplacementErrorMessage(); return; }
            catch (IOException) { FileReplacementErrorMessage(); return; }
        }
        private void CustomUpdaterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cancelled = true;
            Downloader?.CancelAsync();

            Thread.Sleep(1000);
            new TempFolder().Delete();
        }


        private void DownloadErrorMessage()
        {
            MessageBox.Show(LocalizationUI.GetString("DownloadError"), LocalizationUI.GetString("DownloadErrorTitle"), MessageBoxButtons.OK);
            if (Cancelled) return;
            this.SafeInvoke(Close);
        }
        private void FileReplacementErrorMessage()
        {
            MessageBox.Show(LocalizationUI.GetString("FileReplacementError"), LocalizationUI.GetString("FileReplacementErrorTitle"), MessageBoxButtons.OK);
            if (Cancelled) return;
            this.SafeInvoke(Close);
        }
        private void NoUpdateNeededMessage()
        {
            MessageBox.Show(LocalizationUI.GetString("NoUpdateNeeded"), LocalizationUI.GetString("NoUpdateNeededTitle"), MessageBoxButtons.OK);
            if (Cancelled) return;
            this.SafeInvoke(Close);
        }
        private void UpdatedMessage()
        {
            MessageBox.Show(LocalizationUI.GetString("Updated"), LocalizationUI.GetString("UpdatedTitle"), MessageBoxButtons.OK);
            if (Cancelled) return;
            this.SafeInvoke(Close);
        }
    }
}
