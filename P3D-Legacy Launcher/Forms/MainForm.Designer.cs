using System;
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
            this.TabPage_About = new System.Windows.Forms.TabPage();
            this.TextBox_Credits = new System.Windows.Forms.TextBox();
            this.TextBox_About = new System.Windows.Forms.TextBox();
            this.Label_Version = new System.Windows.Forms.Label();
            this.LinkLabel_Pokemon3D = new System.Windows.Forms.LinkLabel();
            this.TabPage_Settings = new System.Windows.Forms.TabPage();
            this.GroupBox__GameJolt = new System.Windows.Forms.GroupBox();
            this.CheckBox_SaveCredentials = new System.Windows.Forms.CheckBox();
            this.CheckBox_AutoLogIn = new System.Windows.Forms.CheckBox();
            this.TextBox_Username = new System.Windows.Forms.TextBox();
            this.Label_Token = new System.Windows.Forms.Label();
            this.Label_Username = new System.Windows.Forms.Label();
            this.GroupBox_Netwok = new System.Windows.Forms.GroupBox();
            this.ComboBox_SelectedDL = new System.Windows.Forms.ComboBox();
            this.Label_SelectedDL = new System.Windows.Forms.Label();
            this.Button_SaveSettings = new System.Windows.Forms.Button();
            this.GroupBox_Startup = new System.Windows.Forms.GroupBox();
            this.Label_Language = new System.Windows.Forms.Label();
            this.ComboBox_Language = new System.Windows.Forms.ComboBox();
            this.Check_Updates = new System.Windows.Forms.CheckBox();
            this.TabPage_Logger = new System.Windows.Forms.TabPage();
            this.TextBox_Logger = new System.Windows.Forms.TextBox();
            this.TabPage_News = new System.Windows.Forms.TabPage();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabPage_FAQ = new System.Windows.Forms.TabPage();
            this.PictureBox_GameJolt = new System.Windows.Forms.PictureBox();
            this.BackgroundWorker_GameJolt = new System.ComponentModel.BackgroundWorker();
            this.PictureBox_GameJolt_Offline = new System.Windows.Forms.PictureBox();
            this.TextBox_Token = new P3D.Legacy.Launcher.Controls.WatermarkTextBox();
            this.TabPage_About.SuspendLayout();
            this.TabPage_Settings.SuspendLayout();
            this.GroupBox__GameJolt.SuspendLayout();
            this.GroupBox_Netwok.SuspendLayout();
            this.GroupBox_Startup.SuspendLayout();
            this.TabPage_Logger.SuspendLayout();
            this.TabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameJolt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameJolt_Offline)).BeginInit();
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
            this.ComboBox_CurrentProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            // TabPage_About
            // 
            this.TabPage_About.BackColor = System.Drawing.Color.White;
            this.TabPage_About.Controls.Add(this.TextBox_Credits);
            this.TabPage_About.Controls.Add(this.TextBox_About);
            this.TabPage_About.Controls.Add(this.Label_Version);
            this.TabPage_About.Controls.Add(this.LinkLabel_Pokemon3D);
            resources.ApplyResources(this.TabPage_About, "TabPage_About");
            this.TabPage_About.Name = "TabPage_About";
            // 
            // TextBox_Credits
            // 
            this.TextBox_Credits.BackColor = System.Drawing.Color.White;
            this.TextBox_Credits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.TextBox_Credits, "TextBox_Credits");
            this.TextBox_Credits.Name = "TextBox_Credits";
            this.TextBox_Credits.ReadOnly = true;
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
            // LinkLabel_Pokemon3D
            // 
            resources.ApplyResources(this.LinkLabel_Pokemon3D, "LinkLabel_Pokemon3D");
            this.LinkLabel_Pokemon3D.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(112)))), ((int)(((byte)(0)))));
            this.LinkLabel_Pokemon3D.Name = "LinkLabel_Pokemon3D";
            this.LinkLabel_Pokemon3D.TabStop = true;
            this.LinkLabel_Pokemon3D.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_Pokemon3D_LinkClicked);
            // 
            // TabPage_Settings
            // 
            this.TabPage_Settings.Controls.Add(this.GroupBox__GameJolt);
            this.TabPage_Settings.Controls.Add(this.GroupBox_Netwok);
            this.TabPage_Settings.Controls.Add(this.Button_SaveSettings);
            this.TabPage_Settings.Controls.Add(this.GroupBox_Startup);
            this.TabPage_Settings.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.TabPage_Settings, "TabPage_Settings");
            this.TabPage_Settings.Name = "TabPage_Settings";
            this.TabPage_Settings.UseVisualStyleBackColor = true;
            // 
            // GroupBox__GameJolt
            // 
            resources.ApplyResources(this.GroupBox__GameJolt, "GroupBox__GameJolt");
            this.GroupBox__GameJolt.Controls.Add(this.TextBox_Token);
            this.GroupBox__GameJolt.Controls.Add(this.CheckBox_SaveCredentials);
            this.GroupBox__GameJolt.Controls.Add(this.CheckBox_AutoLogIn);
            this.GroupBox__GameJolt.Controls.Add(this.TextBox_Username);
            this.GroupBox__GameJolt.Controls.Add(this.Label_Token);
            this.GroupBox__GameJolt.Controls.Add(this.Label_Username);
            this.GroupBox__GameJolt.Name = "GroupBox__GameJolt";
            this.GroupBox__GameJolt.TabStop = false;
            // 
            // CheckBox_SaveCredentials
            // 
            resources.ApplyResources(this.CheckBox_SaveCredentials, "CheckBox_SaveCredentials");
            this.CheckBox_SaveCredentials.Name = "CheckBox_SaveCredentials";
            this.CheckBox_SaveCredentials.UseVisualStyleBackColor = true;
            this.CheckBox_SaveCredentials.CheckedChanged += new System.EventHandler(this.CheckBox_SaveCredentials_CheckedChanged);
            // 
            // CheckBox_AutoLogIn
            // 
            resources.ApplyResources(this.CheckBox_AutoLogIn, "CheckBox_AutoLogIn");
            this.CheckBox_AutoLogIn.Name = "CheckBox_AutoLogIn";
            this.CheckBox_AutoLogIn.UseVisualStyleBackColor = true;
            // 
            // TextBox_Username
            // 
            resources.ApplyResources(this.TextBox_Username, "TextBox_Username");
            this.TextBox_Username.Name = "TextBox_Username";
            // 
            // Label_Token
            // 
            resources.ApplyResources(this.Label_Token, "Label_Token");
            this.Label_Token.Name = "Label_Token";
            // 
            // Label_Username
            // 
            resources.ApplyResources(this.Label_Username, "Label_Username");
            this.Label_Username.Name = "Label_Username";
            // 
            // GroupBox_Netwok
            // 
            resources.ApplyResources(this.GroupBox_Netwok, "GroupBox_Netwok");
            this.GroupBox_Netwok.Controls.Add(this.ComboBox_SelectedDL);
            this.GroupBox_Netwok.Controls.Add(this.Label_SelectedDL);
            this.GroupBox_Netwok.Name = "GroupBox_Netwok";
            this.GroupBox_Netwok.TabStop = false;
            // 
            // ComboBox_SelectedDL
            // 
            resources.ApplyResources(this.ComboBox_SelectedDL, "ComboBox_SelectedDL");
            this.ComboBox_SelectedDL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_SelectedDL.FormattingEnabled = true;
            this.ComboBox_SelectedDL.Name = "ComboBox_SelectedDL";
            // 
            // Label_SelectedDL
            // 
            resources.ApplyResources(this.Label_SelectedDL, "Label_SelectedDL");
            this.Label_SelectedDL.Name = "Label_SelectedDL";
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
            resources.ApplyResources(this.ComboBox_Language, "ComboBox_Language");
            this.ComboBox_Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Language.FormattingEnabled = true;
            this.ComboBox_Language.Name = "ComboBox_Language";
            // 
            // Check_Updates
            // 
            resources.ApplyResources(this.Check_Updates, "Check_Updates");
            this.Check_Updates.Name = "Check_Updates";
            this.Check_Updates.UseVisualStyleBackColor = true;
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
            // TabPage_News
            // 
            resources.ApplyResources(this.TabPage_News, "TabPage_News");
            this.TabPage_News.Name = "TabPage_News";
            this.TabPage_News.UseVisualStyleBackColor = true;
            // 
            // TabControl
            // 
            resources.ApplyResources(this.TabControl, "TabControl");
            this.TabControl.Controls.Add(this.TabPage_News);
            this.TabControl.Controls.Add(this.TabPage_Logger);
            this.TabControl.Controls.Add(this.TabPage_FAQ);
            this.TabControl.Controls.Add(this.TabPage_Settings);
            this.TabControl.Controls.Add(this.TabPage_About);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            // 
            // TabPage_FAQ
            // 
            resources.ApplyResources(this.TabPage_FAQ, "TabPage_FAQ");
            this.TabPage_FAQ.Name = "TabPage_FAQ";
            this.TabPage_FAQ.UseVisualStyleBackColor = true;
            // 
            // PictureBox_GameJolt
            // 
            resources.ApplyResources(this.PictureBox_GameJolt, "PictureBox_GameJolt");
            this.PictureBox_GameJolt.Name = "PictureBox_GameJolt";
            this.PictureBox_GameJolt.TabStop = false;
            this.PictureBox_GameJolt.Click += new System.EventHandler(this.PictureBox_GameJolt_Click);
            // 
            // BackgroundWorker_GameJolt
            // 
            this.BackgroundWorker_GameJolt.WorkerSupportsCancellation = true;
            this.BackgroundWorker_GameJolt.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_GameJolt_DoWork);
            // 
            // PictureBox_GameJolt_Offline
            // 
            resources.ApplyResources(this.PictureBox_GameJolt_Offline, "PictureBox_GameJolt_Offline");
            this.PictureBox_GameJolt_Offline.Name = "PictureBox_GameJolt_Offline";
            this.PictureBox_GameJolt_Offline.TabStop = false;
            this.PictureBox_GameJolt_Offline.Click += new System.EventHandler(this.PictureBox_GameJolt_Offline_Click);
            // 
            // TextBox_Token
            // 
            this.TextBox_Token.Hint = "This is your GameJolt Token, not your GameJolt Password! For more info, check the" +
    " FAQ tab";
            resources.ApplyResources(this.TextBox_Token, "TextBox_Token");
            this.TextBox_Token.Name = "TextBox_Token";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PictureBox_GameJolt);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.Button_DeleteProfile);
            this.Controls.Add(this.Button_EditProfile);
            this.Controls.Add(this.Button_NewProfile);
            this.Controls.Add(this.ComboBox_CurrentProfile);
            this.Controls.Add(this.Button_CheckForUpdates);
            this.Controls.Add(this.Putton_StartGame);
            this.Controls.Add(this.PictureBox_GameJolt_Offline);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.TabPage_About.ResumeLayout(false);
            this.TabPage_About.PerformLayout();
            this.TabPage_Settings.ResumeLayout(false);
            this.GroupBox__GameJolt.ResumeLayout(false);
            this.GroupBox__GameJolt.PerformLayout();
            this.GroupBox_Netwok.ResumeLayout(false);
            this.GroupBox_Netwok.PerformLayout();
            this.GroupBox_Startup.ResumeLayout(false);
            this.GroupBox_Startup.PerformLayout();
            this.TabPage_Logger.ResumeLayout(false);
            this.TabPage_Logger.PerformLayout();
            this.TabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameJolt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameJolt_Offline)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ComboBox ComboBox_CurrentProfile;
        private Button Button_NewProfile;
        private Button Button_EditProfile;
        private Button Button_DeleteProfile;
        private Button Button_CheckForUpdates;
        private Button Putton_StartGame;
        private TabPage TabPage_About;
        private TextBox TextBox_Credits;
        private TextBox TextBox_About;
        private Label Label_Version;
        private LinkLabel LinkLabel_Pokemon3D;
        private TabPage TabPage_Settings;
        private GroupBox GroupBox__GameJolt;
        private TextBox TextBox_Username;
        private Label Label_Token;
        private Label Label_Username;
        private GroupBox GroupBox_Netwok;
        private ComboBox ComboBox_SelectedDL;
        private Label Label_SelectedDL;
        private Button Button_SaveSettings;
        private GroupBox GroupBox_Startup;
        private Label Label_Language;
        private ComboBox ComboBox_Language;
        private CheckBox Check_Updates;
        private TabPage TabPage_Logger;
        private TextBox TextBox_Logger;
        private TabPage TabPage_News;
        private TabControl TabControl;
        private CheckBox CheckBox_SaveCredentials;
        private CheckBox CheckBox_AutoLogIn;
        private Controls.WatermarkTextBox TextBox_Token;
        private TabPage TabPage_FAQ;
        private PictureBox PictureBox_GameJolt;
        private System.ComponentModel.BackgroundWorker BackgroundWorker_GameJolt;
        private PictureBox PictureBox_GameJolt_Offline;
    }
}

