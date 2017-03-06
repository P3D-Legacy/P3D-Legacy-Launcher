using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ionic.Zip;

using Octokit;

using P3D.Legacy.Launcher.Controls;
using P3D.Legacy.Launcher.Extensions;
using P3D.Legacy.Launcher.Services;

using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Forms
{
    internal partial class ReleaseDownloaderForm : LocalizableForm
    {
        private WebClient Downloader { get; set; }

        private ReleaseAsset ReleaseAsset { get; }
        private IFile TempFile => StorageInfo.GetTempFile(ReleaseAsset.Name).Result;
        private IFolder ExtractionFolder { get; }

        public ReleaseDownloaderForm(ReleaseAsset releaseAsset, IFolder extractionFolder)
        {
            ReleaseAsset = releaseAsset;
            ExtractionFolder = extractionFolder;

            InitializeComponent();
        }

        private async void DirectUpdaterForm_Shown(object sender, EventArgs e) => await Task.Run(DownloadFileAsync);

        private async Task DownloadFileAsync()
        {
            await TempFile.DeleteAsync();

            try
            {
                using (Downloader = new WebClient())
                {
                    Downloader.DownloadProgressChanged += client_DownloadProgressChanged;
                    await Downloader.DownloadFileTaskAsync(ReleaseAsset.BrowserDownloadUrl, TempFile.Path);
                }
            }
            catch (WebException) { return; }

            //TODO: Check
            ExtractFile();
        }
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.SafeInvoke(delegate
            {
                double bytesIn = e.BytesReceived;
                double totalBytes = e.TotalBytesToReceive;
                double percentage = bytesIn / totalBytes * 100;
                PercentageProgressBar.Value = (int) percentage;
            });
        }

        private void ExtractFile()
        {
            Label_ProgressBar1.SafeInvoke(() =>
            {
                PercentageProgressBar.Value = 0;
                Label_ProgressBar1.Text = Label_ProgressBar2.Text;
            });

            BackgroundWorker_Extractor.RunWorkerAsync();
        }

        private async void BackgroundWorker_Extractor_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            using(var fs = await TempFile.OpenAsync(PCLExt.FileStorage.FileAccess.Read))
            using (var zip = ZipFile.Read(fs))
            {
                var result = zip.Skip(1).Where(entry => entry.FileName.Contains(zip[0].FileName)).ToList();
                result = result.Select(c =>
                {
                    c.FileName = c.FileName.Replace(zip[0].FileName, "");
                    return c;
                }).ToList();

                // -- We skip the Main folder.
                for (var i = 0; i < result.Count; i++)
                {
                    if (e.Cancel) return;
                    result[i].Extract(ExtractionFolder.Path, ExtractExistingFileAction.OverwriteSilently);

                    double bytesIn = i;
                    double totalBytes = result.Count;
                    double percentage = bytesIn / totalBytes * 100;
                    BackgroundWorker_Extractor.ReportProgress((int) percentage);
                }
            }
        }
        private void BackgroundWorker_Extractor_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e) => this.SafeInvoke(() => PercentageProgressBar.Value = e.ProgressPercentage);
        private void BackgroundWorker_Extractor_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.SafeInvoke(delegate
            {
                Close();
                DialogResult = DialogResult.OK;
            });
        }

        private async void DirectDownloaderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Downloader?.CancelAsync();
            BackgroundWorker_Extractor?.CancelAsync();

            await Task.Delay(1000);
            try { await StorageInfo.TempFolder.DeleteAsync(); }
            catch (IOException) { /* Program.ActionsBeforeExit.Add(() => AsyncExtensions.RunSync(async () => await StorageInfo.TempFolder.DeleteAsync())); */ }
        }
    }
}