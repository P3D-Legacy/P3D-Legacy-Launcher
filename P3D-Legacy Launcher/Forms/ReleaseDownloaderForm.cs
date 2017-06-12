using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

using ICSharpCode.SharpZipLib.Zip;

using Octokit;

using P3D.Legacy.Launcher.Controls;
using P3D.Legacy.Launcher.Extensions;
using P3D.Legacy.Launcher.Storage.Files;
using P3D.Legacy.Launcher.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Forms
{
    internal partial class ReleaseDownloaderForm : LocalizableForm
    {
        private WebClient Downloader { get; set; }

        private ReleaseAsset ReleaseAsset { get; }
        private IFile TempFile => new TempFile(ReleaseAsset.Name);
        private IFolder ExtractionFolder { get; }

        public ReleaseDownloaderForm(ReleaseAsset releaseAsset, IFolder extractionFolder)
        {
            ReleaseAsset = releaseAsset;
            ExtractionFolder = extractionFolder;

            InitializeComponent();
        }

        private void DirectUpdaterForm_Shown(object sender, EventArgs args)
        {
            TempFile.Delete();

            try
            {
                using (Downloader = new WebClient())
                {
                    Downloader.DownloadFileCompleted += (s, e) =>
                    {
                        Label_ProgressBar1.SafeInvoke(() =>
                        {
                            PercentageProgressBar.Value = 0;
                            Label_ProgressBar1.Text = Label_ProgressBar2.Text;
                        });

                        BackgroundWorker_Extractor.RunWorkerAsync();
                    };
                    Downloader.DownloadProgressChanged += (s, e) =>
                    {
                        this.SafeInvoke(delegate
                        {
                            double bytesIn = e.BytesReceived;
                            double totalBytes = e.TotalBytesToReceive;
                            double percentage = bytesIn / totalBytes * 100;
                            PercentageProgressBar.Value = (int) percentage;
                        });
                    };
                    Downloader.DownloadFileAsync(new Uri(ReleaseAsset.BrowserDownloadUrl), TempFile.Path);
                }
            }
            catch (WebException) { }
        }

        private void BackgroundWorker_Extractor_DoWork(object sender, System.ComponentModel.DoWorkEventArgs args)
        {
            using(var fs = TempFile.Open(PCLExt.FileStorage.FileAccess.Read))
            using (var zip = new ZipFile(fs))
            {
                var result = zip.Cast<ZipEntry>().Skip(1).Where(entry => entry.Name.Contains(zip[0].Name)).ToList(); // -- Zip should contain main folder.

                // -- We skip the Main folder.
                for (var i = 0; i < result.Count; i++)
                {
                    if (args.Cancel) return;

                    var zipEntry = result[i];
                    var path = zipEntry.Name.Replace(zip[0].Name, "");

                    if (zipEntry.IsDirectory)
                    {
                        ExtractionFolder.GetFolderFromPath(path);
                    }
                    if (zipEntry.IsFile)
                    {
                        using (var inputStream = zip.GetInputStream(zipEntry))
                        using (var fileStream = ExtractionFolder.GetFileFromPath(path).Open(PCLExt.FileStorage.FileAccess.ReadAndWrite))
                            inputStream.CopyTo(fileStream);
                    }

                    double bytesIn = i;
                    double totalBytes = result.Count;
                    double percentage = bytesIn / totalBytes * 100;
                    BackgroundWorker_Extractor.ReportProgress((int) percentage);
                }
            }
        }
        private void BackgroundWorker_Extractor_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs args) => this.SafeInvoke(() => PercentageProgressBar.Value = args.ProgressPercentage);
        private void BackgroundWorker_Extractor_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs args)
        {
            this.SafeInvoke(delegate
            {
                Close();
                DialogResult = DialogResult.OK;
            });
        }

        private void DirectDownloaderForm_FormClosing(object sender, FormClosingEventArgs args)
        {
            Downloader?.CancelAsync();
            BackgroundWorker_Extractor?.CancelAsync();

            try { new TempFolder().Delete(); }
            catch (FileNotFoundException) { Program.ActionsBeforeExit.Add(() => new TempFolder().Delete()); }
        }
    }
}