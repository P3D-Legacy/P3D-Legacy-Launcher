namespace P3D.Legacy.Launcher.Forms
{
    partial class PerFileUpdaterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PerFileUpdaterForm));
            this.Label_ProgressBar1 = new P3D.Legacy.Launcher.Controls.LocalizableLabel();
            this.Label_ProgressBar2 = new P3D.Legacy.Launcher.Controls.LocalizableLabel();
            this.Label_ProgressBar3 = new P3D.Legacy.Launcher.Controls.LocalizableLabel();
            this.PercentageProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // Label_ProgressBar1
            // 
            resources.ApplyResources(this.Label_ProgressBar1, "Label_ProgressBar1");
            this.Label_ProgressBar1.Name = "Label_ProgressBar1";
            this.Label_ProgressBar1.StringID_Text = "pfu_label_updateinfoprogress";
            // 
            // Label_ProgressBar2
            // 
            resources.ApplyResources(this.Label_ProgressBar2, "Label_ProgressBar2");
            this.Label_ProgressBar2.Name = "Label_ProgressBar2";
            this.Label_ProgressBar2.StringID_Text = "pfu_label_comparisonprogress";
            // 
            // Label_ProgressBar3
            // 
            resources.ApplyResources(this.Label_ProgressBar3, "Label_ProgressBar3");
            this.Label_ProgressBar3.Name = "Label_ProgressBar3";
            this.Label_ProgressBar3.StringID_Text = "pfu_label_downloadprogress";
            // 
            // PercentageProgressBar
            // 
            resources.ApplyResources(this.PercentageProgressBar, "PercentageProgressBar");
            this.PercentageProgressBar.Name = "PercentageProgressBar";
            // 
            // PerFileUpdaterForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PercentageProgressBar);
            this.Controls.Add(this.Label_ProgressBar1);
            this.Controls.Add(this.Label_ProgressBar2);
            this.Controls.Add(this.Label_ProgressBar3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "PerFileUpdaterForm";
            this.StringID_Title = "pfu_title";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomUpdaterForm_FormClosing);
            this.Shown += new System.EventHandler(this.CustomUpdaterForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private P3D.Legacy.Launcher.Controls.LocalizableLabel Label_ProgressBar1;
        private P3D.Legacy.Launcher.Controls.LocalizableLabel Label_ProgressBar2;
        private P3D.Legacy.Launcher.Controls.LocalizableLabel Label_ProgressBar3;
        private System.Windows.Forms.ProgressBar PercentageProgressBar;
    }
}