namespace P3D.Legacy.Launcher.Forms
{
    partial class DirectUpdaterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectUpdaterForm));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Label_ProgressBar1 = new System.Windows.Forms.Label();
            this.Label_ProgressBar2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // Label_ProgressBar1
            // 
            resources.ApplyResources(this.Label_ProgressBar1, "Label_ProgressBar1");
            this.Label_ProgressBar1.Name = "Label_ProgressBar1";
            // 
            // Label_ProgressBar2
            // 
            resources.ApplyResources(this.Label_ProgressBar2, "Label_ProgressBar2");
            this.Label_ProgressBar2.Name = "Label_ProgressBar2";
            // 
            // DirectDownloaderForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Label_ProgressBar1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Label_ProgressBar2);
            this.Name = "DirectDownloaderForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DirectDownloaderForm_FormClosing);
            this.Load += new System.EventHandler(this.DirectDownloaderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label Label_ProgressBar1;
        private System.Windows.Forms.Label Label_ProgressBar2;
    }
}