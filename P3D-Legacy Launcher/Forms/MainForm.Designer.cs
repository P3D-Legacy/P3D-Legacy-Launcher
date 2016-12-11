using System.Windows.Forms;

namespace P3D.Legacy.Launcher.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Button_CheckForUpdates = new System.Windows.Forms.Button();
            this.Putton_StartGame = new System.Windows.Forms.Button();
            this.ComboBox_CurrentProfile = new System.Windows.Forms.ComboBox();
            this.Button_NewProfile = new System.Windows.Forms.Button();
            this.Button_EditProfile = new System.Windows.Forms.Button();
            this.Button_DeleteProfile = new System.Windows.Forms.Button();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabPage_News = new System.Windows.Forms.TabPage();
            this.WebBrowser_News = new System.Windows.Forms.WebBrowser();
            this.TabPage_Logger = new System.Windows.Forms.TabPage();
            this.TextBox_Logger = new System.Windows.Forms.TextBox();
            this.TabPage_Settings = new System.Windows.Forms.TabPage();
            this.Button_SaveSettings = new System.Windows.Forms.Button();
            this.GroupBox_Startup = new System.Windows.Forms.GroupBox();
            this.Label_Language = new System.Windows.Forms.Label();
            this.ComboBox_Language = new System.Windows.Forms.ComboBox();
            this.Check_Updates = new System.Windows.Forms.CheckBox();
            this.TabPage_About = new System.Windows.Forms.TabPage();
            this.LinkLabel_Pokemon3D = new System.Windows.Forms.LinkLabel();
            this.TextBox_About = new System.Windows.Forms.TextBox();
            this.Label_Version = new System.Windows.Forms.Label();
            this.TabControl.SuspendLayout();
            this.TabPage_News.SuspendLayout();
            this.TabPage_Logger.SuspendLayout();
            this.TabPage_Settings.SuspendLayout();
            this.GroupBox_Startup.SuspendLayout();
            this.TabPage_About.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_CheckForUpdates
            // 
            resources.ApplyResources(this.Button_CheckForUpdates, "Button_CheckForUpdates");
            this.Button_CheckForUpdates.Name = "Button_CheckForUpdates";
            this.Button_CheckForUpdates.UseVisualStyleBackColor = true;
            this.Button_CheckForUpdates.Click += new System.EventHandler(this.Button_CheckForUpdates_Click);
            // 
            // Putton_StartGame
            // 
            resources.ApplyResources(this.Putton_StartGame, "Putton_StartGame");
            this.Putton_StartGame.Name = "Putton_StartGame";
            this.Putton_StartGame.UseVisualStyleBackColor = true;
            this.Putton_StartGame.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // ComboBox_CurrentProfile
            // 
            this.ComboBox_CurrentProfile.FormattingEnabled = true;
            resources.ApplyResources(this.ComboBox_CurrentProfile, "ComboBox_CurrentProfile");
            this.ComboBox_CurrentProfile.Name = "ComboBox_CurrentProfile";
            this.ComboBox_CurrentProfile.SelectedIndexChanged += new System.EventHandler(this.ComboBox_CurrentProfile_SelectedIndexChanged);
            // 
            // Button_NewProfile
            // 
            resources.ApplyResources(this.Button_NewProfile, "Button_NewProfile");
            this.Button_NewProfile.Name = "Button_NewProfile";
            this.Button_NewProfile.UseVisualStyleBackColor = true;
            this.Button_NewProfile.Click += new System.EventHandler(this.Button_NewProfile_Click);
            // 
            // Button_EditProfile
            // 
            resources.ApplyResources(this.Button_EditProfile, "Button_EditProfile");
            this.Button_EditProfile.Name = "Button_EditProfile";
            this.Button_EditProfile.UseVisualStyleBackColor = true;
            this.Button_EditProfile.Click += new System.EventHandler(this.Button_EditProfile_Click);
            // 
            // Button_DeleteProfile
            // 
            resources.ApplyResources(this.Button_DeleteProfile, "Button_DeleteProfile");
            this.Button_DeleteProfile.Name = "Button_DeleteProfile";
            this.Button_DeleteProfile.UseVisualStyleBackColor = true;
            this.Button_DeleteProfile.Click += new System.EventHandler(this.Button_DeleteProfile_Click);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabPage_News);
            this.TabControl.Controls.Add(this.TabPage_Logger);
            this.TabControl.Controls.Add(this.TabPage_Settings);
            this.TabControl.Controls.Add(this.TabPage_About);
            resources.ApplyResources(this.TabControl, "TabControl");
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            // 
            // TabPage_News
            // 
            this.TabPage_News.Controls.Add(this.WebBrowser_News);
            resources.ApplyResources(this.TabPage_News, "TabPage_News");
            this.TabPage_News.Name = "TabPage_News";
            this.TabPage_News.UseVisualStyleBackColor = true;
            // 
            // WebBrowser_News
            // 
            resources.ApplyResources(this.WebBrowser_News, "WebBrowser_News");
            this.WebBrowser_News.Name = "WebBrowser_News";
            this.WebBrowser_News.Url = new System.Uri("https://p3d-legacy.github.io/launcher/", System.UriKind.Absolute);
            // 
            // TabPage_Logger
            // 
            this.TabPage_Logger.Controls.Add(this.TextBox_Logger);
            resources.ApplyResources(this.TabPage_Logger, "TabPage_Logger");
            this.TabPage_Logger.Name = "TabPage_Logger";
            this.TabPage_Logger.UseVisualStyleBackColor = true;
            // 
            // TextBox_Logger
            // 
            resources.ApplyResources(this.TextBox_Logger, "TextBox_Logger");
            this.TextBox_Logger.Name = "TextBox_Logger";
            this.TextBox_Logger.ReadOnly = true;
            // 
            // TabPage_Settings
            // 
            this.TabPage_Settings.Controls.Add(this.Button_SaveSettings);
            this.TabPage_Settings.Controls.Add(this.GroupBox_Startup);
            resources.ApplyResources(this.TabPage_Settings, "TabPage_Settings");
            this.TabPage_Settings.Name = "TabPage_Settings";
            this.TabPage_Settings.UseVisualStyleBackColor = true;
            // 
            // Button_SaveSettings
            // 
            resources.ApplyResources(this.Button_SaveSettings, "Button_SaveSettings");
            this.Button_SaveSettings.Name = "Button_SaveSettings";
            this.Button_SaveSettings.UseVisualStyleBackColor = true;
            this.Button_SaveSettings.Click += new System.EventHandler(this.Button_SaveSettings_Click);
            // 
            // GroupBox_Startup
            // 
            resources.ApplyResources(this.GroupBox_Startup, "GroupBox_Startup");
            this.GroupBox_Startup.Controls.Add(this.Label_Language);
            this.GroupBox_Startup.Controls.Add(this.ComboBox_Language);
            this.GroupBox_Startup.Controls.Add(this.Check_Updates);
            this.GroupBox_Startup.Name = "GroupBox_Startup";
            this.GroupBox_Startup.TabStop = false;
            // 
            // Label_Language
            // 
            resources.ApplyResources(this.Label_Language, "Label_Language");
            this.Label_Language.Name = "Label_Language";
            // 
            // ComboBox_Language
            // 
            this.ComboBox_Language.FormattingEnabled = true;
            resources.ApplyResources(this.ComboBox_Language, "ComboBox_Language");
            this.ComboBox_Language.Name = "ComboBox_Language";
            // 
            // Check_Updates
            // 
            resources.ApplyResources(this.Check_Updates, "Check_Updates");
            this.Check_Updates.Name = "Check_Updates";
            this.Check_Updates.UseVisualStyleBackColor = true;
            // 
            // TabPage_About
            // 
            this.TabPage_About.Controls.Add(this.Label_Version);
            this.TabPage_About.Controls.Add(this.LinkLabel_Pokemon3D);
            this.TabPage_About.Controls.Add(this.TextBox_About);
            resources.ApplyResources(this.TabPage_About, "TabPage_About");
            this.TabPage_About.Name = "TabPage_About";
            this.TabPage_About.UseVisualStyleBackColor = true;
            // 
            // LinkLabel_Pokemon3D
            // 
            resources.ApplyResources(this.LinkLabel_Pokemon3D, "LinkLabel_Pokemon3D");
            this.LinkLabel_Pokemon3D.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(112)))), ((int)(((byte)(0)))));
            this.LinkLabel_Pokemon3D.Name = "LinkLabel_Pokemon3D";
            this.LinkLabel_Pokemon3D.TabStop = true;
            this.LinkLabel_Pokemon3D.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_Pokemon3D_LinkClicked);
            // 
            // TextBox_About
            // 
            this.TextBox_About.BackColor = System.Drawing.Color.White;
            this.TextBox_About.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.TextBox_About, "TextBox_About");
            this.TextBox_About.Name = "TextBox_About";
            this.TextBox_About.ReadOnly = true;
            // 
            // Label_Version
            // 
            resources.ApplyResources(this.Label_Version, "Label_Version");
            this.Label_Version.Name = "Label_Version";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.Button_DeleteProfile);
            this.Controls.Add(this.Button_EditProfile);
            this.Controls.Add(this.Button_NewProfile);
            this.Controls.Add(this.ComboBox_CurrentProfile);
            this.Controls.Add(this.Button_CheckForUpdates);
            this.Controls.Add(this.Putton_StartGame);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.TabControl.ResumeLayout(false);
            this.TabPage_News.ResumeLayout(false);
            this.TabPage_Logger.ResumeLayout(false);
            this.TabPage_Logger.PerformLayout();
            this.TabPage_Settings.ResumeLayout(false);
            this.GroupBox_Startup.ResumeLayout(false);
            this.GroupBox_Startup.PerformLayout();
            this.TabPage_About.ResumeLayout(false);
            this.TabPage_About.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ComboBox ComboBox_CurrentProfile;
        private Button Button_NewProfile;
        private Button Button_EditProfile;
        private Button Button_DeleteProfile;
        private TabControl TabControl;
        private TabPage TabPage_News;
        private TabPage TabPage_Logger;
        private TextBox TextBox_Logger;
        private TabPage TabPage_Settings;
        private TabPage TabPage_About;
        private WebBrowser WebBrowser_News;
        private Button Button_CheckForUpdates;
        private Button Putton_StartGame;
        private CheckBox Check_Updates;
        private LinkLabel LinkLabel_Pokemon3D;
        private TextBox TextBox_About;
        private GroupBox GroupBox_Startup;
        private Label Label_Language;
        private ComboBox ComboBox_Language;
        private Button Button_SaveSettings;
        private Label Label_Version;
    }
}

