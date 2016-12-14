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
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.Label_ProgressBar2 = new System.Windows.Forms.Label();
            this.Label_ProgressBar3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label_ProgressBar1
            // 
            this.Label_ProgressBar1.AutoSize = true;
            this.Label_ProgressBar1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label_ProgressBar1.Location = new System.Drawing.Point(12, 9);
            this.Label_ProgressBar1.Name = "Label_ProgressBar1";
            this.Label_ProgressBar1.Size = new System.Drawing.Size(110, 13);
            this.Label_ProgressBar1.TabIndex = 4;
            this.Label_ProgressBar1.Text = "Update Info Progress:";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ProgressBar.Location = new System.Drawing.Point(15, 25);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(257, 25);
            this.ProgressBar.TabIndex = 3;
            // 
            // Label_ProgressBar2
            // 
            this.Label_ProgressBar2.AutoSize = true;
            this.Label_ProgressBar2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label_ProgressBar2.Location = new System.Drawing.Point(13, 9);
            this.Label_ProgressBar2.Name = "Label_ProgressBar2";
            this.Label_ProgressBar2.Size = new System.Drawing.Size(109, 13);
            this.Label_ProgressBar2.TabIndex = 5;
            this.Label_ProgressBar2.Text = "Comparsion Progress:";
            this.Label_ProgressBar2.Visible = false;
            // 
            // Label_ProgressBar3
            // 
            this.Label_ProgressBar3.AutoSize = true;
            this.Label_ProgressBar3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label_ProgressBar3.Location = new System.Drawing.Point(13, 9);
            this.Label_ProgressBar3.Name = "Label_ProgressBar3";
            this.Label_ProgressBar3.Size = new System.Drawing.Size(102, 13);
            this.Label_ProgressBar3.TabIndex = 6;
            this.Label_ProgressBar3.Text = "Download Progress:";
            this.Label_ProgressBar3.Visible = false;
            // 
            // CustomUpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 62);
            this.Controls.Add(this.Label_ProgressBar1);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.Label_ProgressBar2);
            this.Controls.Add(this.Label_ProgressBar3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(300, 100);
            this.MinimumSize = new System.Drawing.Size(300, 100);
            this.Name = "CustomUpdaterForm";
            this.Text = "NAppUpdater";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NAppUpdaterForm_FormClosing);
            this.Shown += new System.EventHandler(this.CustomUpdaterForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_ProgressBar1;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label Label_ProgressBar2;
        private System.Windows.Forms.Label Label_ProgressBar3;
    }
}