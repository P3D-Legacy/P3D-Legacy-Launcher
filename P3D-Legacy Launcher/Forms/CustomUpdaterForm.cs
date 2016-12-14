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

using P3D.Legacy.Launcher.Data;

namespace P3D.Legacy.Launcher.Forms
{
    public partial class CustomUpdaterForm : Form
    {
        private SynchronizationContext SynchronizationContext { get; } =  SynchronizationContext.Current;

        private WebClient Downloader { get; set; }
        private bool Cancelled { get; set; }

        private ReleaseAsset UpdateInfoAsset { get; }
        private string ReleaseInfoFilePath => FileSystemInfo.TempFilePath(UpdateInfoAsset.Name);
        private string UpdatedFolderPath { get; }
        private Uri DLUri { get; }
        private long DLSize { get; set; }

        public CustomUpdaterForm(ReleaseAsset updateInfoAsset, string updatedFolderPath, Uri dlUri)
        {
            UpdateInfoAsset = updateInfoAsset;
            UpdatedFolderPath = updatedFolderPath;
            DLUri = dlUri;

            InitializeComponent();
        }

        private async void CustomUpdaterForm_Shown(object sender, EventArgs e)
        {
            if (!Directory.Exists(FileSystemInfo.TempFolderPath))
                Directory.CreateDirectory(FileSystemInfo.TempFolderPath);

            await Task.Run(DownloadFile);
            Close();
        }

        private async Task DownloadFile()
        {
            if (File.Exists(ReleaseInfoFilePath))
                File.Delete(ReleaseInfoFilePath);

            DLSize = UpdateInfoAsset.Size;

            try
            {
                using (Downloader = new WebClient())
                {
                    Downloader.DownloadProgressChanged += client_DownloadProgressChanged;
                    await Downloader.DownloadFileTaskAsync(UpdateInfoAsset.BrowserDownloadUrl, ReleaseInfoFilePath);
                    Downloader.DownloadProgressChanged -= client_DownloadProgressChanged;
                }
            }
            catch (Exception) { DownloadErrorMessage(); return; }
            if (Cancelled) return;


            if (File.Exists(ReleaseInfoFilePath))
            {
                var list = StartUpdate();
                if (Cancelled) return;

                if (list.Any())
                {
                    await UpdateFiles(list);
                    if (Cancelled) return;
                    UpdatedMessage();
                }
                else
                    NoUpdateNeededMessage();
            }
            else
                DownloadErrorMessage();
        }

        private List<UpdateFileEntry> StartUpdate()
        {
            if (Cancelled) return new List<UpdateFileEntry>();

            SynchronizationContext.Send(obj => Label_ProgressBar1.Text = Label_ProgressBar2.Text, null);

            var releaseInfoContent = File.ReadAllText(ReleaseInfoFilePath);
            var deserializer = UpdateInfo.DeserializerBuilder.Build();
            var updateInfo = deserializer.Deserialize<UpdateInfo>(releaseInfoContent);

            var crc32 = new Crc32();
            var sha1 = new SHA1Managed();
            var notValidFileEntries = new List<UpdateFileEntry>();
            SynchronizationContext.Send(delegate { ProgressBar.Value = 0; ProgressBar.Step = 1; ProgressBar.Maximum = updateInfo.Files.Count; }, null);
            foreach (var updateFileEntry in updateInfo.Files)
            {
                if (Cancelled) return new List<UpdateFileEntry>();

                SynchronizationContext.Send(obj => ProgressBar.PerformStep(), null);

                var filePath = Path.Combine(UpdatedFolderPath, updateFileEntry.AbsoluteFilePath);
                if (!File.Exists(filePath))
                {
                    notValidFileEntries.Add(updateFileEntry);
                    continue;
                }
                using (var fs = File.Open(filePath, System.IO.FileMode.Open, FileAccess.Read))
                {
                    var crc32Hash = string.Empty;
                    var sha1Hash = string.Empty;
                    crc32Hash = crc32.ComputeHash(fs)
                        .Aggregate(crc32Hash, (current, b) => current + b.ToString("x2").ToLower());
                    if (crc32Hash == updateFileEntry.CRC32)
                    {
                        sha1Hash = sha1.ComputeHash(fs)
                            .Aggregate(sha1Hash, (current, b) => current + b.ToString("x2").ToLower());
                        if (sha1Hash == updateFileEntry.SHA1)
                            continue;
                        else
                        {
                            notValidFileEntries.Add(updateFileEntry);
                            continue;
                        }
                    }
                    else
                    {
                        notValidFileEntries.Add(updateFileEntry);
                        continue;
                    }
                }
            }

            return notValidFileEntries;
        }

        private async Task UpdateFiles(List<UpdateFileEntry> updateFileEntries)
        {
            if (Cancelled) return;

            SynchronizationContext.Send(obj => Label_ProgressBar1.Text = Label_ProgressBar3.Text, null);

            try
            {
                DLSize = updateFileEntries.Sum(updateFileEntry => updateFileEntry.Size);
                foreach (var updateFileEntry in updateFileEntries)
                {
                    if (Cancelled) return;

                    var dlUri = new Uri(DLUri, updateFileEntry.AbsoluteFilePath);
                    var tempFilePath = FileSystemInfo.TempFilePath(updateFileEntry.AbsoluteFilePath);

                    using (Downloader = new WebClient())
                    {
                        Downloader.DownloadProgressChanged += client_DownloadProgressChanged;
                        await Downloader.DownloadFileTaskAsync(dlUri, tempFilePath);
                        Downloader.DownloadProgressChanged -= client_DownloadProgressChanged;
                    }
                }
            }
            catch (Exception) { DownloadErrorMessage(); return; }

            ReplaceFiles(updateFileEntries);
        }
        private void ReplaceFiles(List<UpdateFileEntry> updateFileEntries)
        {
            if (Cancelled) return;

            try
            {
                foreach (var updateFileEntry in updateFileEntries)
                {
                    if (Cancelled) return;

                    var tempFilePath = FileSystemInfo.TempFilePath(updateFileEntry.AbsoluteFilePath);
                    var filePath = Path.Combine(UpdatedFolderPath, updateFileEntry.AbsoluteFilePath);

                    if (File.Exists(filePath))
                        File.Delete(filePath);
                    File.Move(tempFilePath, filePath);
                    File.Delete(tempFilePath);
                }
            }
            catch (Exception) { FileReplacementErrorMessage(); return; }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            SynchronizationContext.Send(delegate
            {
                ProgressBar.Maximum = (int) DLSize;
                ProgressBar.Value = (int) e.BytesReceived;
            }, null);
        }

        private void NAppUpdaterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Downloader.DownloadProgressChanged -= client_DownloadProgressChanged;
            Downloader?.CancelAsync();
            Cancelled = true;
        }


        private void DownloadErrorMessage()
        {
            MessageBox.Show(MBLang.DownloadError, MBLang.DownloadErrorTitle, MessageBoxButtons.OK);
            if (Cancelled) return;
            SynchronizationContext.Send(obj => Close(), null);
        }
        private void FileReplacementErrorMessage()
        {
            MessageBox.Show(MBLang.FileReplacementError, MBLang.FileReplacementErrorTitle, MessageBoxButtons.OK);
            if (Cancelled) return;
            SynchronizationContext.Send(obj => Close(), null);
        }
        private void NoUpdateNeededMessage()
        {
            MessageBox.Show(MBLang.NoUpdateNeeded, MBLang.NoUpdateNeededTitle, MessageBoxButtons.OK);
            if (Cancelled) return;
            SynchronizationContext.Send(obj => Close(), null);
        }
        private void UpdatedMessage()
        {
            MessageBox.Show(MBLang.Updated, MBLang.UpdatedTitle, MessageBoxButtons.OK);
            if (Cancelled) return;
            SynchronizationContext.Send(obj => Close(), null);
        }
    }
}
