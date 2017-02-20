namespace P3D.Legacy.Launcher.Forms
{
    partial class CustomUpdaterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomUpdaterForm));
            this.Label_ProgressBar1 = new System.Windows.Forms.Label();
            this.Label_ProgressBar2 = new System.Windows.Forms.Label();
            this.Label_ProgressBar3 = new System.Windows.Forms.Label();
            this.PercentageProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
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
            // Label_ProgressBar3
            // 
            resources.ApplyResources(this.Label_ProgressBar3, "Label_ProgressBar3");
            this.Label_ProgressBar3.Name = "Label_ProgressBar3";
            // 
            // PercentageProgressBar
            // 
            resources.ApplyResources(this.PercentageProgressBar, "PercentageProgressBar");
            this.PercentageProgressBar.Name = "PercentageProgressBar";
            // 
            // CustomUpdaterForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PercentageProgressBar);
            this.Controls.Add(this.Label_ProgressBar1);
            this.Controls.Add(this.Label_ProgressBar2);
            this.Controls.Add(this.Label_ProgressBar3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CustomUpdaterForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomUpdaterForm_FormClosing);
            this.Shown += new System.EventHandler(this.CustomUpdaterForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_ProgressBar1;
        private System.Windows.Forms.Label Label_ProgressBar2;
        private System.Windows.Forms.Label Label_ProgressBar3;
        private System.Windows.Forms.ProgressBar PercentageProgressBar;
    }
}