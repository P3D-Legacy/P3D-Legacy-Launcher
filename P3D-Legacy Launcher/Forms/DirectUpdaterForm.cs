using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ionic.Zip;

using Octokit;
using P3D.Legacy.Launcher.Extensions;

namespace P3D.Legacy.Launcher.Forms
{
    public partial class DirectUpdaterForm : Form
    {
        private WebClient Downloader { get; set; }
        private bool Cancelled { get; set; }

        private ReleaseAsset ReleaseAsset { get; }
        private string TempFilePath => FileSystemInfo.TempFilePath(ReleaseAsset.Name);
        private string ExtractionFolder { get; }

        private int _totalFiles;
        private int _filesExtracted;


        public DirectUpdaterForm(ReleaseAsset releaseAsset, string extractionFolder)
        {
            ReleaseAsset = releaseAsset;
            ExtractionFolder = extractionFolder;

            InitializeComponent();
        }

        private async void DirectUpdaterForm_Shown(object sender, EventArgs e)
        {
            if (!Directory.Exists(FileSystemInfo.TempFolderPath))
                Directory.CreateDirectory(FileSystemInfo.TempFolderPath);

            await Task.Run(DownloadFile);
            Close();
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
                    Downloader.DownloadProgressChanged -= client_DownloadProgressChanged;
                }
            }
            catch (Exception) { return; }
            if (Cancelled) return;

            if (File.Exists(TempFilePath))
                ExtractFile();
            else
                ;
        }
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.SafeInvoke(delegate { ProgressBar.Maximum = ReleaseAsset.Size; ProgressBar.Value = (int) e.BytesReceived; });
        }

        private void ExtractFile()
        {
            if (Cancelled) return;

            this.SafeInvoke(() => Label_ProgressBar1.Text = Label_ProgressBar2.Text);

            if (!Directory.Exists(ExtractionFolder))
                Directory.CreateDirectory(ExtractionFolder);

            using (var zip = ZipFile.Read(TempFilePath))
            {
                zip.ExtractProgress += zip_ExtractProgress;

                var result = zip.Skip(1).Where(entry => entry.FileName.Contains(zip[0].FileName)).ToList();
                result = result.Select(c =>
                {
                    c.FileName = c.FileName.Replace(zip[0].FileName, "");
                    return c;
                }).ToList();

                _totalFiles = result.Count;
                _filesExtracted = 0;

                // -- We skip the Main folder.
                foreach (var zipEntry in result)
                {
                    if (Cancelled) return;
                    zipEntry.Extract(ExtractionFolder, ExtractExistingFileAction.OverwriteSilently);
                }
            }

            File.Delete(TempFilePath);
        }

        private void zip_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            this.SafeInvoke(delegate
            {
                if (e.EventType != ZipProgressEventType.Extracting_BeforeExtractEntry)
                    return;
                _filesExtracted++;

                ProgressBar.Maximum = _totalFiles;
                ProgressBar.Value = _filesExtracted;
            });
        }

        private async void DirectDownloaderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Downloader.DownloadProgressChanged -= client_DownloadProgressChanged;
            Downloader?.CancelAsync();
            Cancelled = true;

            if (File.Exists(TempFilePath))
            {
                var x = 0;
                while (x < 10 && IsFileLocked(TempFilePath)) // -- Wait only 1 second.
                {
                    await Task.Delay(100);
                    x++;
                }
                File.Delete(TempFilePath);
            }
        }
        private static bool IsFileLocked(string path)
        {
            FileStream stream = null;

            try { stream = File.Open(path, System.IO.FileMode.Open, FileAccess.ReadWrite, FileShare.None); }
            catch (IOException) { return true; }
            finally { stream?.Close(); }

            return false;
        }
    }
}