using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ionic.Zip;

using Octokit;

using P3D.Legacy.Launcher.Extensions;
using P3D.Legacy.Launcher.Services;

namespace P3D.Legacy.Launcher.Forms
{
    internal partial class DirectUpdaterForm : Form
    {
        private WebClient Downloader { get; set; }

        private ReleaseAsset ReleaseAsset { get; }
        private string TempFilePath => FileSystem.TempFilePath(ReleaseAsset.Name);
        private string ExtractionFolder { get; }

        public DirectUpdaterForm(ReleaseAsset releaseAsset, string extractionFolder)
        {
            ReleaseAsset = releaseAsset;
            ExtractionFolder = extractionFolder;

            InitializeComponent();
        }

        private async void DirectUpdaterForm_Shown(object sender, EventArgs e)
        {
            if (!Directory.Exists(FileSystem.TempFolderPath))
                Directory.CreateDirectory(FileSystem.TempFolderPath);

            await Task.Run(DownloadFile);
        }

        private async Task DownloadFile()
        {
            if (File.Exists(TempFilePath))
                File.Delete(TempFilePath);

            try
            {
                using (Downloader = new WebClient())
                {
                    Downloader.DownloadProgressChanged += client_DownloadProgressChanged;
                    await Downloader.DownloadFileTaskAsync(ReleaseAsset.BrowserDownloadUrl, TempFilePath);
                }
            }
            catch (WebException) { return; }

            if (File.Exists(TempFilePath))
                ExtractFile();
            else
                ;
        }
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.SafeInvoke(delegate
            {
                PercentageProgressBar.Maximum = ReleaseAsset.Size;
                PercentageProgressBar.Value = (int) e.BytesReceived;
            });
        }

        private void ExtractFile()
        {
            Label_ProgressBar1.SafeInvoke(() => Label_ProgressBar1.Text = Label_ProgressBar2.Text);

            if (!Directory.Exists(ExtractionFolder))
                Directory.CreateDirectory(ExtractionFolder);

            BackgroundWorker_Extractor.RunWorkerAsync();
        }

        private void BackgroundWorker_Extractor_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            using (var zip = ZipFile.Read(TempFilePath))
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
                    result[i].Extract(ExtractionFolder, ExtractExistingFileAction.OverwriteSilently);
                    BackgroundWorker_Extractor.ReportProgress((int) Math.Round((double) i / (double) result.Count * 100));
                }
            }
        }
        private void BackgroundWorker_Extractor_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.SafeInvoke(delegate
            {
                PercentageProgressBar.Maximum = 100;
                PercentageProgressBar.Value = e.ProgressPercentage;
            });
        }
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
            Directory.Delete(FileSystem.TempFolderPath, true);
        }
    }
}