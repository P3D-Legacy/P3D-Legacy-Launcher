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
            this.Label_ProgressBar1.AutoSize = true;
            this.Label_ProgressBar1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label_ProgressBar1.Location = new System.Drawing.Point(12, 9);
            this.Label_ProgressBar1.Name = "Label_ProgressBar1";
            this.Label_ProgressBar1.Size = new System.Drawing.Size(146, 13);
            this.Label_ProgressBar1.TabIndex = 4;
            this.Label_ProgressBar1.Text = "pfu_label_updateinfoprogress";
            // 
            // Label_ProgressBar2
            // 
            this.Label_ProgressBar2.AutoSize = true;
            this.Label_ProgressBar2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label_ProgressBar2.Location = new System.Drawing.Point(13, 9);
            this.Label_ProgressBar2.Name = "Label_ProgressBar2";
            this.Label_ProgressBar2.Size = new System.Drawing.Size(150, 13);
            this.Label_ProgressBar2.TabIndex = 5;
            this.Label_ProgressBar2.Text = "pfu_label_comparisonprogress";
            this.Label_ProgressBar2.Visible = false;
            // 
            // Label_ProgressBar3
            // 
            this.Label_ProgressBar3.AutoSize = true;
            this.Label_ProgressBar3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label_ProgressBar3.Location = new System.Drawing.Point(13, 9);
            this.Label_ProgressBar3.Name = "Label_ProgressBar3";
            this.Label_ProgressBar3.Size = new System.Drawing.Size(142, 13);
            this.Label_ProgressBar3.TabIndex = 6;
            this.Label_ProgressBar3.Text = "pfu_label_downloadprogress";
            this.Label_ProgressBar3.Visible = false;
            // 
            // PercentageProgressBar
            // 
            this.PercentageProgressBar.Location = new System.Drawing.Point(12, 25);
            this.PercentageProgressBar.Name = "PercentageProgressBar";
            this.PercentageProgressBar.Size = new System.Drawing.Size(260, 23);
            this.PercentageProgressBar.TabIndex = 0;
            // 
            // PerFileUpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 66);
            this.Controls.Add(this.PercentageProgressBar);
            this.Controls.Add(this.Label_ProgressBar1);
            this.Controls.Add(this.Label_ProgressBar2);
            this.Controls.Add(this.Label_ProgressBar3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(300, 100);
            this.MinimumSize = new System.Drawing.Size(300, 100);
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