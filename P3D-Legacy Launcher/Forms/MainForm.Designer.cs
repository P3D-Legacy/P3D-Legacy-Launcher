using System.ComponentModel;
using System.Windows.Forms;
using P3D.Legacy.Launcher.Controls;

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
            this.Button_CheckForUpdates = new P3D.Legacy.Launcher.Controls.LocalizableButton();
            this.Button_StartGame = new P3D.Legacy.Launcher.Controls.LocalizableButton();
            this.ComboBox_CurrentProfile = new System.Windows.Forms.ComboBox();
            this.Button_NewProfile = new P3D.Legacy.Launcher.Controls.LocalizableButton();
            this.Button_EditProfile = new P3D.Legacy.Launcher.Controls.LocalizableButton();
            this.Button_DeleteProfile = new P3D.Legacy.Launcher.Controls.LocalizableButton();
            this.TabPage_About = new P3D.Legacy.Launcher.Controls.LocalizableTabPage();
            this.Label_Credits = new P3D.Legacy.Launcher.Controls.LocalizableLabel();
            this.Label_About = new P3D.Legacy.Launcher.Controls.LocalizableLabel();
            this.Label_Version = new System.Windows.Forms.Label();
            this.LinkLabel_Pokemon3D = new System.Windows.Forms.LinkLabel();
            this.TabPage_Settings = new P3D.Legacy.Launcher.Controls.LocalizableTabPage();
            this.GroupBox_Game = new P3D.Legacy.Launcher.Controls.LocalizableGroupBox();
            this.CheckBox_CheckServerOnStart = new P3D.Legacy.Launcher.Controls.LocalizableCheckBox();
            this.GroupBox__GameJolt = new P3D.Legacy.Launcher.Controls.LocalizableGroupBox();
            this.TextBox_Token = new P3D.Legacy.Launcher.Controls.LocalizableWatermarkTextBox();
            this.CheckBox_SaveCredentials = new P3D.Legacy.Launcher.Controls.LocalizableCheckBox();
            this.CheckBox_AutoLogIn = new P3D.Legacy.Launcher.Controls.LocalizableCheckBox();
            this.TextBox_Username = new System.Windows.Forms.TextBox();
            this.Label_Token = new P3D.Legacy.Launcher.Controls.LocalizableLabel();
            this.Label_Username = new P3D.Legacy.Launcher.Controls.LocalizableLabel();
            this.GroupBox_Netwok = new P3D.Legacy.Launcher.Controls.LocalizableGroupBox();
            this.ComboBox_SelectedDL = new System.Windows.Forms.ComboBox();
            this.Label_SelectedDL = new P3D.Legacy.Launcher.Controls.LocalizableLabel();
            this.Button_SaveSettings = new P3D.Legacy.Launcher.Controls.LocalizableButton();
            this.GroupBox_Startup = new P3D.Legacy.Launcher.Controls.LocalizableGroupBox();
            this.Label_Language = new P3D.Legacy.Launcher.Controls.LocalizableLabel();
            this.ComboBox_Language = new System.Windows.Forms.ComboBox();
            this.Check_Updates = new P3D.Legacy.Launcher.Controls.LocalizableCheckBox();
            this.TabPage_Logger = new P3D.Legacy.Launcher.Controls.LocalizableTabPage();
            this.TextBox_Logger = new System.Windows.Forms.TextBox();
            this.TabPage_News = new P3D.Legacy.Launcher.Controls.LocalizableTabPage();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabPage_FAQ = new P3D.Legacy.Launcher.Controls.LocalizableTabPage();
            this.TabPage_Skins = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Label_NoGameJolt = new P3D.Legacy.Launcher.Controls.LocalizableLabel();
            this.PictureBox_GameJolt = new System.Windows.Forms.PictureBox();
            this.BackgroundWorker_GameJolt = new System.ComponentModel.BackgroundWorker();
            this.PictureBox_GameJolt_Offline = new System.Windows.Forms.PictureBox();
            this.TabPage_About.SuspendLayout();
            this.TabPage_Settings.SuspendLayout();
            this.GroupBox_Game.SuspendLayout();
            this.GroupBox__GameJolt.SuspendLayout();
            this.GroupBox_Netwok.SuspendLayout();
            this.GroupBox_Startup.SuspendLayout();
            this.TabPage_Logger.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.TabPage_Skins.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameJolt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameJolt_Offline)).BeginInit();
            this.SuspendLayout();
            // 
            // Button_CheckForUpdates
            // 
            this.Button_CheckForUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_CheckForUpdates.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Button_CheckForUpdates.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button_CheckForUpdates.Location = new System.Drawing.Point(682, 445);
            this.Button_CheckForUpdates.Name = "Button_CheckForUpdates";
            this.Button_CheckForUpdates.Size = new System.Drawing.Size(150, 25);
            this.Button_CheckForUpdates.TabIndex = 6;
            this.Button_CheckForUpdates.Text = "mf_button_checkforupdates";
            this.Button_CheckForUpdates.UseVisualStyleBackColor = true;
            this.Button_CheckForUpdates.Click += new System.EventHandler(this.Button_CheckForUpdates_Click);
            // 
            // Button_StartGame
            // 
            this.Button_StartGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_StartGame.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.Button_StartGame.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button_StartGame.Location = new System.Drawing.Point(682, 414);
            this.Button_StartGame.Name = "Button_StartGame";
            this.Button_StartGame.Size = new System.Drawing.Size(150, 25);
            this.Button_StartGame.TabIndex = 5;
            this.Button_StartGame.Text = "mf_button_play";
            this.Button_StartGame.UseVisualStyleBackColor = true;
            this.Button_StartGame.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // ComboBox_CurrentProfile
            // 
            this.ComboBox_CurrentProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_CurrentProfile.FormattingEnabled = true;
            this.ComboBox_CurrentProfile.Location = new System.Drawing.Point(12, 414);
            this.ComboBox_CurrentProfile.Name = "ComboBox_CurrentProfile";
            this.ComboBox_CurrentProfile.Size = new System.Drawing.Size(462, 21);
            this.ComboBox_CurrentProfile.TabIndex = 1;
            this.ComboBox_CurrentProfile.SelectedIndexChanged += new System.EventHandler(this.ComboBox_CurrentProfile_SelectedIndexChanged);
            // 
            // Button_NewProfile
            // 
            this.Button_NewProfile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button_NewProfile.Location = new System.Drawing.Point(12, 445);
            this.Button_NewProfile.Name = "Button_NewProfile";
            this.Button_NewProfile.Size = new System.Drawing.Size(150, 25);
            this.Button_NewProfile.TabIndex = 2;
            this.Button_NewProfile.Text = "mf_button_newprofile";
            this.Button_NewProfile.UseVisualStyleBackColor = true;
            this.Button_NewProfile.Click += new System.EventHandler(this.Button_NewProfile_Click);
            // 
            // Button_EditProfile
            // 
            this.Button_EditProfile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button_EditProfile.Location = new System.Drawing.Point(168, 445);
            this.Button_EditProfile.Name = "Button_EditProfile";
            this.Button_EditProfile.Size = new System.Drawing.Size(150, 25);
            this.Button_EditProfile.TabIndex = 3;
            this.Button_EditProfile.Text = "mf_button_editprofile";
            this.Button_EditProfile.UseVisualStyleBackColor = true;
            this.Button_EditProfile.Click += new System.EventHandler(this.Button_EditProfile_Click);
            // 
            // Button_DeleteProfile
            // 
            this.Button_DeleteProfile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button_DeleteProfile.Location = new System.Drawing.Point(324, 445);
            this.Button_DeleteProfile.Name = "Button_DeleteProfile";
            this.Button_DeleteProfile.Size = new System.Drawing.Size(150, 25);
            this.Button_DeleteProfile.TabIndex = 4;
            this.Button_DeleteProfile.Text = "mf_button_delete";
            this.Button_DeleteProfile.UseVisualStyleBackColor = true;
            this.Button_DeleteProfile.Click += new System.EventHandler(this.Button_DeleteProfile_Click);
            // 
            // TabPage_About
            // 
            this.TabPage_About.BackColor = System.Drawing.Color.White;
            this.TabPage_About.Controls.Add(this.Label_Credits);
            this.TabPage_About.Controls.Add(this.Label_About);
            this.TabPage_About.Controls.Add(this.Label_Version);
            this.TabPage_About.Controls.Add(this.LinkLabel_Pokemon3D);
            this.TabPage_About.Location = new System.Drawing.Point(4, 22);
            this.TabPage_About.Name = "TabPage_About";
            this.TabPage_About.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_About.Size = new System.Drawing.Size(836, 382);
            this.TabPage_About.TabIndex = 3;
            this.TabPage_About.Text = "mf_tabpage_about";
            // 
            // Label_Credits
            // 
            this.Label_Credits.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label_Credits.Location = new System.Drawing.Point(3, 329);
            this.Label_Credits.Name = "Label_Credits";
            this.Label_Credits.Size = new System.Drawing.Size(830, 50);
            this.Label_Credits.TabIndex = 2;
            this.Label_Credits.Text = "mf_textbox_credits";
            // 
            // Label_About
            // 
            this.Label_About.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_About.Location = new System.Drawing.Point(3, 20);
            this.Label_About.Name = "Label_About";
            this.Label_About.Size = new System.Drawing.Size(830, 150);
            this.Label_About.TabIndex = 2;
            this.Label_About.Text = resources.GetString("Label_About.Text");
            // 
            // Label_Version
            // 
            this.Label_Version.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Version.AutoSize = true;
            this.Label_Version.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label_Version.Location = new System.Drawing.Point(793, 6);
            this.Label_Version.Name = "Label_Version";
            this.Label_Version.Size = new System.Drawing.Size(37, 13);
            this.Label_Version.TabIndex = 12;
            this.Label_Version.Text = "          ";
            // 
            // LinkLabel_Pokemon3D
            // 
            this.LinkLabel_Pokemon3D.Dock = System.Windows.Forms.DockStyle.Top;
            this.LinkLabel_Pokemon3D.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.LinkLabel_Pokemon3D.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LinkLabel_Pokemon3D.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(112)))), ((int)(((byte)(0)))));
            this.LinkLabel_Pokemon3D.Location = new System.Drawing.Point(3, 3);
            this.LinkLabel_Pokemon3D.Name = "LinkLabel_Pokemon3D";
            this.LinkLabel_Pokemon3D.Size = new System.Drawing.Size(830, 17);
            this.LinkLabel_Pokemon3D.TabIndex = 1;
            this.LinkLabel_Pokemon3D.TabStop = true;
            this.LinkLabel_Pokemon3D.Text = "Pokémon3D";
            this.LinkLabel_Pokemon3D.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_Pokemon3D_LinkClicked);
            // 
            // TabPage_Settings
            // 
            this.TabPage_Settings.Controls.Add(this.GroupBox_Game);
            this.TabPage_Settings.Controls.Add(this.GroupBox__GameJolt);
            this.TabPage_Settings.Controls.Add(this.GroupBox_Netwok);
            this.TabPage_Settings.Controls.Add(this.Button_SaveSettings);
            this.TabPage_Settings.Controls.Add(this.GroupBox_Startup);
            this.TabPage_Settings.Cursor = System.Windows.Forms.Cursors.Default;
            this.TabPage_Settings.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Settings.Name = "TabPage_Settings";
            this.TabPage_Settings.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Settings.Size = new System.Drawing.Size(836, 382);
            this.TabPage_Settings.TabIndex = 2;
            this.TabPage_Settings.Text = "mf_tabpage_settings";
            // 
            // GroupBox_Game
            // 
            this.GroupBox_Game.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Game.Controls.Add(this.CheckBox_CheckServerOnStart);
            this.GroupBox_Game.Location = new System.Drawing.Point(8, 218);
            this.GroupBox_Game.Name = "GroupBox_Game";
            this.GroupBox_Game.Size = new System.Drawing.Size(822, 58);
            this.GroupBox_Game.TabIndex = 4;
            this.GroupBox_Game.TabStop = false;
            this.GroupBox_Game.Text = "mf_groupbox_game";
            // 
            // CheckBox_CheckServerOnStart
            // 
            this.CheckBox_CheckServerOnStart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CheckBox_CheckServerOnStart.Location = new System.Drawing.Point(6, 19);
            this.CheckBox_CheckServerOnStart.Name = "CheckBox_CheckServerOnStart";
            this.CheckBox_CheckServerOnStart.Size = new System.Drawing.Size(807, 17);
            this.CheckBox_CheckServerOnStart.TabIndex = 1;
            this.CheckBox_CheckServerOnStart.Text = "mf_checkbox_checkserveronstart";
            this.CheckBox_CheckServerOnStart.UseVisualStyleBackColor = true;
            // 
            // GroupBox__GameJolt
            // 
            this.GroupBox__GameJolt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox__GameJolt.Controls.Add(this.TextBox_Token);
            this.GroupBox__GameJolt.Controls.Add(this.CheckBox_SaveCredentials);
            this.GroupBox__GameJolt.Controls.Add(this.CheckBox_AutoLogIn);
            this.GroupBox__GameJolt.Controls.Add(this.TextBox_Username);
            this.GroupBox__GameJolt.Controls.Add(this.Label_Token);
            this.GroupBox__GameJolt.Controls.Add(this.Label_Username);
            this.GroupBox__GameJolt.Location = new System.Drawing.Point(8, 92);
            this.GroupBox__GameJolt.Name = "GroupBox__GameJolt";
            this.GroupBox__GameJolt.Size = new System.Drawing.Size(822, 120);
            this.GroupBox__GameJolt.TabIndex = 1;
            this.GroupBox__GameJolt.TabStop = false;
            this.GroupBox__GameJolt.Text = "mf_groupbox_gamejolt";
            // 
            // TextBox_Token
            // 
            this.TextBox_Token.Hint = "mf_label_token_hint";
            this.TextBox_Token.Location = new System.Drawing.Point(216, 45);
            this.TextBox_Token.Name = "TextBox_Token";
            this.TextBox_Token.PasswordChar = '*';
            this.TextBox_Token.Size = new System.Drawing.Size(600, 20);
            this.TextBox_Token.TabIndex = 3;
            // 
            // CheckBox_SaveCredentials
            // 
            this.CheckBox_SaveCredentials.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CheckBox_SaveCredentials.Location = new System.Drawing.Point(9, 74);
            this.CheckBox_SaveCredentials.Name = "CheckBox_SaveCredentials";
            this.CheckBox_SaveCredentials.Size = new System.Drawing.Size(201, 17);
            this.CheckBox_SaveCredentials.TabIndex = 4;
            this.CheckBox_SaveCredentials.Text = "mf_checkbox_savecredentials";
            this.CheckBox_SaveCredentials.UseVisualStyleBackColor = true;
            this.CheckBox_SaveCredentials.CheckedChanged += new System.EventHandler(this.CheckBox_SaveCredentials_CheckedChanged);
            // 
            // CheckBox_AutoLogIn
            // 
            this.CheckBox_AutoLogIn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CheckBox_AutoLogIn.Location = new System.Drawing.Point(9, 97);
            this.CheckBox_AutoLogIn.Name = "CheckBox_AutoLogIn";
            this.CheckBox_AutoLogIn.Size = new System.Drawing.Size(201, 17);
            this.CheckBox_AutoLogIn.TabIndex = 5;
            this.CheckBox_AutoLogIn.Text = "mf_checkbox_autologin";
            this.CheckBox_AutoLogIn.UseVisualStyleBackColor = true;
            // 
            // TextBox_Username
            // 
            this.TextBox_Username.Location = new System.Drawing.Point(216, 19);
            this.TextBox_Username.Name = "TextBox_Username";
            this.TextBox_Username.Size = new System.Drawing.Size(600, 20);
            this.TextBox_Username.TabIndex = 1;
            // 
            // Label_Token
            // 
            this.Label_Token.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label_Token.Location = new System.Drawing.Point(6, 48);
            this.Label_Token.Name = "Label_Token";
            this.Label_Token.Size = new System.Drawing.Size(204, 13);
            this.Label_Token.TabIndex = 2;
            this.Label_Token.Text = "mf_label_token";
            // 
            // Label_Username
            // 
            this.Label_Username.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label_Username.Location = new System.Drawing.Point(6, 22);
            this.Label_Username.Name = "Label_Username";
            this.Label_Username.Size = new System.Drawing.Size(204, 13);
            this.Label_Username.TabIndex = 0;
            this.Label_Username.Text = "mf_label_username";
            // 
            // GroupBox_Netwok
            // 
            this.GroupBox_Netwok.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Netwok.Controls.Add(this.ComboBox_SelectedDL);
            this.GroupBox_Netwok.Controls.Add(this.Label_SelectedDL);
            this.GroupBox_Netwok.Enabled = false;
            this.GroupBox_Netwok.Location = new System.Drawing.Point(8, 282);
            this.GroupBox_Netwok.Name = "GroupBox_Netwok";
            this.GroupBox_Netwok.Size = new System.Drawing.Size(822, 58);
            this.GroupBox_Netwok.TabIndex = 2;
            this.GroupBox_Netwok.TabStop = false;
            this.GroupBox_Netwok.Text = "mf_groupbox_network";
            this.GroupBox_Netwok.Visible = false;
            // 
            // ComboBox_SelectedDL
            // 
            this.ComboBox_SelectedDL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_SelectedDL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_SelectedDL.FormattingEnabled = true;
            this.ComboBox_SelectedDL.Location = new System.Drawing.Point(216, 19);
            this.ComboBox_SelectedDL.Name = "ComboBox_SelectedDL";
            this.ComboBox_SelectedDL.Size = new System.Drawing.Size(600, 21);
            this.ComboBox_SelectedDL.TabIndex = 1;
            // 
            // Label_SelectedDL
            // 
            this.Label_SelectedDL.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label_SelectedDL.Location = new System.Drawing.Point(6, 22);
            this.Label_SelectedDL.Name = "Label_SelectedDL";
            this.Label_SelectedDL.Size = new System.Drawing.Size(204, 13);
            this.Label_SelectedDL.TabIndex = 0;
            this.Label_SelectedDL.Text = "mf_label_selecteddl";
            // 
            // Button_SaveSettings
            // 
            this.Button_SaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_SaveSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button_SaveSettings.Location = new System.Drawing.Point(680, 353);
            this.Button_SaveSettings.Name = "Button_SaveSettings";
            this.Button_SaveSettings.Size = new System.Drawing.Size(150, 23);
            this.Button_SaveSettings.TabIndex = 3;
            this.Button_SaveSettings.Text = "mf_button_save";
            this.Button_SaveSettings.UseVisualStyleBackColor = true;
            this.Button_SaveSettings.Click += new System.EventHandler(this.Button_SaveSettings_Click);
            // 
            // GroupBox_Startup
            // 
            this.GroupBox_Startup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Startup.Controls.Add(this.Label_Language);
            this.GroupBox_Startup.Controls.Add(this.ComboBox_Language);
            this.GroupBox_Startup.Controls.Add(this.Check_Updates);
            this.GroupBox_Startup.Location = new System.Drawing.Point(8, 6);
            this.GroupBox_Startup.Name = "GroupBox_Startup";
            this.GroupBox_Startup.Size = new System.Drawing.Size(822, 80);
            this.GroupBox_Startup.TabIndex = 0;
            this.GroupBox_Startup.TabStop = false;
            this.GroupBox_Startup.Text = "mf_groupbox_common";
            // 
            // Label_Language
            // 
            this.Label_Language.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label_Language.Location = new System.Drawing.Point(6, 45);
            this.Label_Language.Name = "Label_Language";
            this.Label_Language.Size = new System.Drawing.Size(204, 13);
            this.Label_Language.TabIndex = 1;
            this.Label_Language.Text = "mf_label_language";
            // 
            // ComboBox_Language
            // 
            this.ComboBox_Language.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBox_Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox_Language.FormattingEnabled = true;
            this.ComboBox_Language.Location = new System.Drawing.Point(216, 42);
            this.ComboBox_Language.Name = "ComboBox_Language";
            this.ComboBox_Language.Size = new System.Drawing.Size(600, 21);
            this.ComboBox_Language.TabIndex = 2;
            // 
            // Check_Updates
            // 
            this.Check_Updates.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Check_Updates.Location = new System.Drawing.Point(9, 19);
            this.Check_Updates.Name = "Check_Updates";
            this.Check_Updates.Size = new System.Drawing.Size(807, 17);
            this.Check_Updates.TabIndex = 0;
            this.Check_Updates.Text = "mf_checkbox_checkforupdatesonstartup";
            this.Check_Updates.UseVisualStyleBackColor = true;
            // 
            // TabPage_Logger
            // 
            this.TabPage_Logger.Controls.Add(this.TextBox_Logger);
            this.TabPage_Logger.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Logger.Name = "TabPage_Logger";
            this.TabPage_Logger.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Logger.Size = new System.Drawing.Size(836, 382);
            this.TabPage_Logger.TabIndex = 1;
            this.TabPage_Logger.Text = "mf_tabpage_logger";
            // 
            // TextBox_Logger
            // 
            this.TextBox_Logger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox_Logger.Location = new System.Drawing.Point(3, 3);
            this.TextBox_Logger.Multiline = true;
            this.TextBox_Logger.Name = "TextBox_Logger";
            this.TextBox_Logger.ReadOnly = true;
            this.TextBox_Logger.Size = new System.Drawing.Size(830, 376);
            this.TextBox_Logger.TabIndex = 0;
            // 
            // TabPage_News
            // 
            this.TabPage_News.Location = new System.Drawing.Point(4, 22);
            this.TabPage_News.Name = "TabPage_News";
            this.TabPage_News.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_News.Size = new System.Drawing.Size(836, 382);
            this.TabPage_News.TabIndex = 0;
            this.TabPage_News.Text = "mf_tabpage_news";
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.TabPage_News);
            this.TabControl.Controls.Add(this.TabPage_Logger);
            this.TabControl.Controls.Add(this.TabPage_FAQ);
            this.TabControl.Controls.Add(this.TabPage_Settings);
            this.TabControl.Controls.Add(this.TabPage_About);
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(844, 408);
            this.TabControl.TabIndex = 0;
            // 
            // TabPage_FAQ
            // 
            this.TabPage_FAQ.Location = new System.Drawing.Point(4, 22);
            this.TabPage_FAQ.Name = "TabPage_FAQ";
            this.TabPage_FAQ.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_FAQ.Size = new System.Drawing.Size(836, 382);
            this.TabPage_FAQ.TabIndex = 4;
            this.TabPage_FAQ.Text = "mf_tabpage_faq";
            // 
            // TabPage_Skins
            // 
            this.TabPage_Skins.Controls.Add(this.pictureBox1);
            this.TabPage_Skins.Controls.Add(this.Label_NoGameJolt);
            this.TabPage_Skins.Location = new System.Drawing.Point(4, 22);
            this.TabPage_Skins.Name = "TabPage_Skins";
            this.TabPage_Skins.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Skins.Size = new System.Drawing.Size(836, 382);
            this.TabPage_Skins.TabIndex = 5;
            this.TabPage_Skins.Text = "mf_tabpage_skins";
            this.TabPage_Skins.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(120, 96);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Label_NoGameJolt
            // 
            this.Label_NoGameJolt.AutoSize = true;
            this.Label_NoGameJolt.Location = new System.Drawing.Point(8, 3);
            this.Label_NoGameJolt.Name = "Label_NoGameJolt";
            this.Label_NoGameJolt.Size = new System.Drawing.Size(103, 13);
            this.Label_NoGameJolt.TabIndex = 0;
            this.Label_NoGameJolt.Text = "mf_label_nogamejolt";
            // 
            // PictureBox_GameJolt
            // 
            this.PictureBox_GameJolt.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PictureBox_GameJolt.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_GameJolt.Image")));
            this.PictureBox_GameJolt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PictureBox_GameJolt.Location = new System.Drawing.Point(638, 423);
            this.PictureBox_GameJolt.Name = "PictureBox_GameJolt";
            this.PictureBox_GameJolt.Size = new System.Drawing.Size(38, 38);
            this.PictureBox_GameJolt.TabIndex = 12;
            this.PictureBox_GameJolt.TabStop = false;
            this.PictureBox_GameJolt.Visible = false;
            this.PictureBox_GameJolt.Click += new System.EventHandler(this.PictureBox_GameJolt_Click);
            // 
            // BackgroundWorker_GameJolt
            // 
            this.BackgroundWorker_GameJolt.WorkerSupportsCancellation = true;
            this.BackgroundWorker_GameJolt.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_GameJolt_DoWork);
            // 
            // PictureBox_GameJolt_Offline
            // 
            this.PictureBox_GameJolt_Offline.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PictureBox_GameJolt_Offline.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_GameJolt_Offline.Image")));
            this.PictureBox_GameJolt_Offline.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PictureBox_GameJolt_Offline.Location = new System.Drawing.Point(638, 423);
            this.PictureBox_GameJolt_Offline.Name = "PictureBox_GameJolt_Offline";
            this.PictureBox_GameJolt_Offline.Size = new System.Drawing.Size(38, 38);
            this.PictureBox_GameJolt_Offline.TabIndex = 13;
            this.PictureBox_GameJolt_Offline.TabStop = false;
            this.PictureBox_GameJolt_Offline.Visible = false;
            this.PictureBox_GameJolt_Offline.Click += new System.EventHandler(this.PictureBox_GameJolt_Offline_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 482);
            this.Controls.Add(this.PictureBox_GameJolt);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.Button_DeleteProfile);
            this.Controls.Add(this.Button_EditProfile);
            this.Controls.Add(this.Button_NewProfile);
            this.Controls.Add(this.ComboBox_CurrentProfile);
            this.Controls.Add(this.Button_CheckForUpdates);
            this.Controls.Add(this.Button_StartGame);
            this.Controls.Add(this.PictureBox_GameJolt_Offline);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(860, 520);
            this.MinimumSize = new System.Drawing.Size(860, 520);
            this.Name = "MainForm";
            this.StringID_Title = "mf_title";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.TabPage_About.ResumeLayout(false);
            this.TabPage_About.PerformLayout();
            this.TabPage_Settings.ResumeLayout(false);
            this.GroupBox_Game.ResumeLayout(false);
            this.GroupBox__GameJolt.ResumeLayout(false);
            this.GroupBox__GameJolt.PerformLayout();
            this.GroupBox_Netwok.ResumeLayout(false);
            this.GroupBox_Startup.ResumeLayout(false);
            this.TabPage_Logger.ResumeLayout(false);
            this.TabPage_Logger.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.TabPage_Skins.ResumeLayout(false);
            this.TabPage_Skins.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameJolt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameJolt_Offline)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ComboBox ComboBox_CurrentProfile;
        private LocalizableButton Button_NewProfile;
        private LocalizableButton Button_EditProfile;
        private LocalizableButton Button_DeleteProfile;
        private LocalizableButton Button_CheckForUpdates;
        private LocalizableButton Button_StartGame;
        private LocalizableTabPage TabPage_About;
        private Label Label_Version;
        private LinkLabel LinkLabel_Pokemon3D;
        private LocalizableTabPage TabPage_Settings;
        private LocalizableGroupBox GroupBox__GameJolt;
        private TextBox TextBox_Username;
        private LocalizableLabel Label_Token;
        private LocalizableLabel Label_Username;
        private LocalizableGroupBox GroupBox_Netwok;
        private ComboBox ComboBox_SelectedDL;
        private LocalizableLabel Label_SelectedDL;
        private LocalizableButton Button_SaveSettings;
        private LocalizableGroupBox GroupBox_Startup;
        private LocalizableLabel Label_Language;
        private ComboBox ComboBox_Language;
        private LocalizableCheckBox Check_Updates;
        private LocalizableTabPage TabPage_Logger;
        private TextBox TextBox_Logger;
        private LocalizableTabPage TabPage_News;
        private TabControl TabControl;
        private LocalizableCheckBox CheckBox_SaveCredentials;
        private LocalizableCheckBox CheckBox_AutoLogIn;
        private LocalizableWatermarkTextBox TextBox_Token;
        private LocalizableTabPage TabPage_FAQ;
        private PictureBox PictureBox_GameJolt;
        private BackgroundWorker BackgroundWorker_GameJolt;
        private PictureBox PictureBox_GameJolt_Offline;
        private LocalizableLabel Label_About;
        private LocalizableLabel Label_Credits;
        private LocalizableGroupBox GroupBox_Game;
        private LocalizableCheckBox CheckBox_CheckServerOnStart;
        private TabPage TabPage_Skins;
        private LocalizableLabel Label_NoGameJolt;
        private PictureBox pictureBox1;
    }
}

