using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

using DamienG.Security.Cryptography;

using Octokit;

using P3D.Legacy.Launcher.Controls;
using P3D.Legacy.Launcher.Data;
using P3D.Legacy.Launcher.Extensions;
using P3D.Legacy.Launcher.Services;

using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Forms
{
    internal partial class PerFileUpdaterForm : LocalizableForm
    {
        private WebClient Downloader { get; set; }
        private bool Cancelled { get; set; }

        private ReleaseAsset UpdateInfoAsset { get; }
        private IFile ReleaseInfoFile => StorageInfo.GetTempFile(UpdateInfoAsset.Name).Result;
        private IFolder UpdatedFolder { get; }
        private Uri DLUri { get; }

        public PerFileUpdaterForm(ReleaseAsset updateInfoAsset, IFolder updatedFolder, Uri dlUri)
        {
            UpdateInfoAsset = updateInfoAsset;
            UpdatedFolder = updatedFolder;
            DLUri = dlUri;

            InitializeComponent();
        }

        private async void CustomUpdaterForm_Shown(object sender, EventArgs e)
        {
            await Task.Run(DownloadFileAsync);
            Close();
        }

        private async Task DownloadFileAsync()
        {
            await ReleaseInfoFile.DeleteAsync();

            try
            {
                PercentageProgressBar.SafeInvoke(() => PercentageProgressBar.Maximum = UpdateInfoAsset.Size);
                using (Downloader = new WebClient())
                {
                    Downloader.DownloadProgressChanged += client_DownloadProgressChanged;
                    await Downloader.DownloadFileTaskAsync(UpdateInfoAsset.BrowserDownloadUrl, ReleaseInfoFile.Path);
                }
            }
            catch (WebException) { DownloadErrorMessage(); return; }
            if (Cancelled) return;


            // TODO: Check
            //if (File.Exists(ReleaseInfoFilePath))
            //{
                var list = await StartUpdateAsync();
                if (Cancelled) return;

                if (list.Any())
                {
                    UpdateFiles(list);
                    if (Cancelled) return;
                    UpdatedMessage();
                }
                else
                    NoUpdateNeededMessage();
            //}
            //else
            //    DownloadErrorMessage();
        }
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            PercentageProgressBar.SafeInvoke(() => PercentageProgressBar.Value = (int) e.BytesReceived);
        }

        private async Task<List<UpdateFileEntryYaml>> StartUpdateAsync()
        {
            if (Cancelled) return new List<UpdateFileEntryYaml>();

            Label_ProgressBar1.SafeInvoke(() => Label_ProgressBar1.Text = Label_ProgressBar2.Text);

            var releaseInfoContent = await ReleaseInfoFile.ReadAllTextAsync();
            var updateInfo = UpdateInfoYaml.Deserialize(releaseInfoContent);

            var crc32 = new Crc32();
            var sha1 = new SHA1Managed();
            var notValidFileEntries = new List<UpdateFileEntryYaml>();
            PercentageProgressBar.SafeInvoke(delegate { PercentageProgressBar.Maximum = updateInfo.Count(); PercentageProgressBar.Step = 1; PercentageProgressBar.Value = 0; });
            Parallel.ForEach(updateInfo, async (updateFileEntry, state) =>
            {
                if(Cancelled) state.Stop();

                PercentageProgressBar.SafeInvoke(() => PercentageProgressBar.PerformStep());

                if (await UpdatedFolder.CheckExistsAsync(updateFileEntry.AbsoluteFilePath) != ExistenceCheckResult.FileExists)
                {
                    notValidFileEntries.Add(updateFileEntry);
                    return;
                }
                using (var fs = await (await UpdatedFolder.GetFileAsync(updateFileEntry.AbsoluteFilePath)).OpenAsync(PCLExt.FileStorage.FileAccess.Read))
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
                Parallel.ForEach(updateFileEntries, async (updateFileEntry, state) =>
                {
                    if (Cancelled) state.Stop();

                    var dlUri = new Uri(DLUri, updateFileEntry.AbsoluteFilePath);
                    var tempFile = await StorageInfo.GetTempFile(updateFileEntry.AbsoluteFilePath);
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
                Parallel.ForEach(updateFileEntries, async (updateFileEntry, state) =>
                {
                    if (Cancelled) state.Break();

                    var tempFilePath = await StorageInfo.GetTempFile(updateFileEntry.AbsoluteFilePath);
                    await tempFilePath.MoveAsync(PortablePath.Combine(UpdatedFolder.Path, updateFileEntry.AbsoluteFilePath));
                    await tempFilePath.DeleteAsync();
                });
            }
            catch (UnauthorizedAccessException) { FileReplacementErrorMessage(); return; }
            catch (IOException) { FileReplacementErrorMessage(); return; }
        }
        private async void CustomUpdaterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cancelled = true;
            Downloader?.CancelAsync();

            await Task.Delay(1000);
            await StorageInfo.TempFolder.DeleteAsync();
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
