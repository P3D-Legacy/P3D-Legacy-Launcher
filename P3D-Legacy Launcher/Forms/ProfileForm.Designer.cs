namespace P3D.Legacy.Launcher.Forms
{
    partial class ProfileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileForm));
            this.GroupBox_ProfileInfo = new System.Windows.Forms.GroupBox();
            this.ComboBox_Version = new System.Windows.Forms.ComboBox();
            this.Label_Version = new System.Windows.Forms.Label();
            this.TextBox_ProfileName = new System.Windows.Forms.TextBox();
            this.Label_ProfileName = new System.Windows.Forms.Label();
            this.Button_SaveProfile = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.GroupBox_Modifications = new System.Windows.Forms.GroupBox();
            this.GroupBox_ProfileInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox_ProfileInfo
            // 
            resources.ApplyResources(this.GroupBox_ProfileInfo, "GroupBox_ProfileInfo");
            this.GroupBox_ProfileInfo.Controls.Add(this.ComboBox_Version);
            this.GroupBox_ProfileInfo.Controls.Add(this.Label_Version);
            this.GroupBox_ProfileInfo.Controls.Add(this.TextBox_ProfileName);
            this.GroupBox_ProfileInfo.Controls.Add(this.Label_ProfileName);
            this.GroupBox_ProfileInfo.Name = "GroupBox_ProfileInfo";
            this.GroupBox_ProfileInfo.TabStop = false;
            // 
            // ComboBox_Version
            // 
            resources.ApplyResources(this.ComboBox_Version, "ComboBox_Version");
            this.ComboBox_Version.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Version.FormattingEnabled = true;
            this.ComboBox_Version.Name = "ComboBox_Version";
            // 
            // Label_Version
            // 
            resources.ApplyResources(this.Label_Version, "Label_Version");
            this.Label_Version.Name = "Label_Version";
            // 
            // TextBox_ProfileName
            // 
            resources.ApplyResources(this.TextBox_ProfileName, "TextBox_ProfileName");
            this.TextBox_ProfileName.Name = "TextBox_ProfileName";
            // 
            // Label_ProfileName
            // 
            resources.ApplyResources(this.Label_ProfileName, "Label_ProfileName");
            this.Label_ProfileName.Name = "Label_ProfileName";
            // 
            // Button_SaveProfile
            // 
            resources.ApplyResources(this.Button_SaveProfile, "Button_SaveProfile");
            this.Button_SaveProfile.Name = "Button_SaveProfile";
            this.Button_SaveProfile.UseVisualStyleBackColor = true;
            this.Button_SaveProfile.Click += new System.EventHandler(this.Button_SaveProfile_Click);
            // 
            // Button_Cancel
            // 
            resources.ApplyResources(this.Button_Cancel, "Button_Cancel");
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // GroupBox_Modifications
            // 
            resources.ApplyResources(this.GroupBox_Modifications, "GroupBox_Modifications");
            this.GroupBox_Modifications.Name = "GroupBox_Modifications";
            this.GroupBox_Modifications.TabStop = false;
            // 
            // ProfileForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupBox_Modifications);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_SaveProfile);
            this.Controls.Add(this.GroupBox_ProfileInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ProfileForm";
            this.GroupBox_ProfileInfo.ResumeLayout(false);
            this.GroupBox_ProfileInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBox_ProfileInfo;
        private System.Windows.Forms.Button Button_SaveProfile;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.ComboBox ComboBox_Version;
        private System.Windows.Forms.Label Label_Version;
        private System.Windows.Forms.TextBox TextBox_ProfileName;
        private System.Windows.Forms.Label Label_ProfileName;
        private System.Windows.Forms.GroupBox GroupBox_Modifications;
    }
}