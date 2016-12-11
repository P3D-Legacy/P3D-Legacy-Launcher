using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;

using Ionic.Zip;

using Octokit;

using FileMode = System.IO.FileMode;

namespace P3D.Legacy.Launcher.Forms
{
    public partial class DirectDownloaderForm : Form
    {
        private WebClient Downloader { get; } = new WebClient();
        private Thread Extractor { get; set; }
        private bool Cancelled { get; set; }

        private ReleaseAsset ReleaseAsset { get; }
        private string TempFilePath => FileSystemInfo.TempFilePath(ReleaseAsset.Name);
        private string ExtractionFolder { get; }

        private int _totalFiles;
        private int _filesExtracted;


        public DirectDownloaderForm(ReleaseAsset releaseAsset, string extractionFolder)
        {
            ReleaseAsset = releaseAsset;
            ExtractionFolder = extractionFolder;

            InitializeComponent();
        }
        private void DirectDownloaderForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(FileSystemInfo.TempFolderPath))
                Directory.CreateDirectory(FileSystemInfo.TempFolderPath);

            DownloadFile();
        }

        private void DownloadFile()
        {
            if (File.Exists(TempFilePath))
                File.Delete(TempFilePath);

            Downloader.DownloadProgressChanged += client_DownloadProgressChanged;
            Downloader.DownloadFileAsync(new Uri(ReleaseAsset.BrowserDownloadUrl), TempFilePath);
            Downloader.DownloadFileCompleted += client_DownloadFileCompleted;
        }
        private void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if(!Cancelled)
                ExtractFile();
        }
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (Cancelled || Disposing || IsDisposed)
                return;

            Invoke((MethodInvoker) delegate
            {
                progressBar1.Maximum = ReleaseAsset.Size / 100;
                progressBar1.Value = (int) e.BytesReceived / 100;
            });
        }

        private void ExtractFile()
        {
            Label_ProgressBar1.Text = Label_ProgressBar2.Text;

            if (!Directory.Exists(ExtractionFolder))
                Directory.CreateDirectory(ExtractionFolder);

            if(Extractor != null && Extractor.ThreadState != ThreadState.Stopped)
                Extractor.Abort();
            Extractor = new Thread(() =>
            {
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
                    foreach (var entry in result)
                        entry.Extract(ExtractionFolder, ExtractExistingFileAction.OverwriteSilently);
                }

                File.Delete(TempFilePath);


                if (Cancelled || Disposing || IsDisposed)
                    return;
                Invoke((MethodInvoker) Close);
            });
            Extractor.Start();
        }
        private void zip_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (Cancelled || Disposing || IsDisposed)
                return;

            Invoke((MethodInvoker) delegate
            {
                if (e.EventType != ZipProgressEventType.Extracting_BeforeExtractEntry)
                    return;
                _filesExtracted++;

                progressBar1.Maximum = _totalFiles / 100;
                progressBar1.Value = _filesExtracted / 100;
            });
        }

        private void DirectDownloaderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cancelled = true;
            Downloader.CancelAsync();

            if (File.Exists(TempFilePath))
            {
                var x = 0;
                while (x < 10 && IsFileLocked(TempFilePath)) // -- Wait only 1 second.
                {
                    Thread.Sleep(100);
                    x++;
                }
                File.Delete(TempFilePath);
            }
        }
        private static bool IsFileLocked(string path)
        {
            FileStream stream = null;

            try { stream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None); }
            catch (IOException) { return true; }
            finally { stream?.Close(); }

            return false;
        }
    }
}