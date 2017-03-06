namespace P3D.Legacy.Launcher.Forms
{
    partial class ReleaseDownloaderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReleaseDownloaderForm));
            this.Label_ProgressBar1 = new P3D.Legacy.Launcher.Controls.LocalizableLabel();
            this.Label_ProgressBar2 = new P3D.Legacy.Launcher.Controls.LocalizableLabel();
            this.PercentageProgressBar = new System.Windows.Forms.ProgressBar();
            this.BackgroundWorker_Extractor = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // Label_ProgressBar1
            // 
            resources.ApplyResources(this.Label_ProgressBar1, "Label_ProgressBar1");
            this.Label_ProgressBar1.Name = "Label_ProgressBar1";
            this.Label_ProgressBar1.StringID_Text = "rd_label_downloadprogress";
            // 
            // Label_ProgressBar2
            // 
            resources.ApplyResources(this.Label_ProgressBar2, "Label_ProgressBar2");
            this.Label_ProgressBar2.Name = "Label_ProgressBar2";
            this.Label_ProgressBar2.StringID_Text = "rd_label_extractionprogress";
            // 
            // PercentageProgressBar
            // 
            resources.ApplyResources(this.PercentageProgressBar, "PercentageProgressBar");
            this.PercentageProgressBar.Name = "PercentageProgressBar";
            // 
            // BackgroundWorker_Extractor
            // 
            this.BackgroundWorker_Extractor.WorkerReportsProgress = true;
            this.BackgroundWorker_Extractor.WorkerSupportsCancellation = true;
            this.BackgroundWorker_Extractor.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Extractor_DoWork);
            this.BackgroundWorker_Extractor.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Extractor_ProgressChanged);
            this.BackgroundWorker_Extractor.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_Extractor_RunWorkerCompleted);
            // 
            // ReleaseDownloaderForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PercentageProgressBar);
            this.Controls.Add(this.Label_ProgressBar1);
            this.Controls.Add(this.Label_ProgressBar2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ReleaseDownloaderForm";
            this.StringID_Title = "rd_title";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DirectDownloaderForm_FormClosing);
            this.Shown += new System.EventHandler(this.DirectUpdaterForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private P3D.Legacy.Launcher.Controls.LocalizableLabel Label_ProgressBar1;
        private P3D.Legacy.Launcher.Controls.LocalizableLabel Label_ProgressBar2;
        private System.Windows.Forms.ProgressBar PercentageProgressBar;
        private System.ComponentModel.BackgroundWorker BackgroundWorker_Extractor;
    }
}