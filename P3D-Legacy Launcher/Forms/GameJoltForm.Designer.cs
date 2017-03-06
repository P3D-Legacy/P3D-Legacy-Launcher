namespace P3D.Legacy.Launcher.Forms
{
    partial class GameJoltForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameJoltForm));
            this.Button_LogIn = new P3D.Legacy.Launcher.Controls.LocalizableButton();
            this.Button_SignIn = new P3D.Legacy.Launcher.Controls.LocalizableButton();
            this.Button_PlayOffline = new P3D.Legacy.Launcher.Controls.LocalizableButton();
            this.PictureBox_GameJoltLogo = new System.Windows.Forms.PictureBox();
            this.Label_QuestionToken = new P3D.Legacy.Launcher.Controls.LocalizableLabel();
            this.WatermarkTextBox_Username = new P3D.Legacy.Launcher.Controls.LocalizableWatermarkTextBox();
            this.WatermarkTextBox_Token = new P3D.Legacy.Launcher.Controls.LocalizableWatermarkTextBox();
            this.CheckBox_SaveCredentials = new P3D.Legacy.Launcher.Controls.LocalizableCheckBox();
            this.CheckBox_AutoLogIn = new P3D.Legacy.Launcher.Controls.LocalizableCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameJoltLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // Button_LogIn
            // 
            resources.ApplyResources(this.Button_LogIn, "Button_LogIn");
            this.Button_LogIn.Name = "Button_LogIn";
            this.Button_LogIn.StringID_Text = "gj_button_login";
            this.Button_LogIn.UseVisualStyleBackColor = true;
            this.Button_LogIn.Click += new System.EventHandler(this.Button_LogIn_Click);
            // 
            // Button_SignIn
            // 
            resources.ApplyResources(this.Button_SignIn, "Button_SignIn");
            this.Button_SignIn.Name = "Button_SignIn";
            this.Button_SignIn.StringID_Text = "gj_button_signin";
            this.Button_SignIn.UseVisualStyleBackColor = true;
            this.Button_SignIn.Click += new System.EventHandler(this.Button_SignIn_Click);
            // 
            // Button_PlayOffline
            // 
            resources.ApplyResources(this.Button_PlayOffline, "Button_PlayOffline");
            this.Button_PlayOffline.Name = "Button_PlayOffline";
            this.Button_PlayOffline.StringID_Text = "gj_button_playoffline";
            this.Button_PlayOffline.UseVisualStyleBackColor = true;
            this.Button_PlayOffline.Click += new System.EventHandler(this.Button_PlayOffline_Click);
            // 
            // PictureBox_GameJoltLogo
            // 
            resources.ApplyResources(this.PictureBox_GameJoltLogo, "PictureBox_GameJoltLogo");
            this.PictureBox_GameJoltLogo.Name = "PictureBox_GameJoltLogo";
            this.PictureBox_GameJoltLogo.TabStop = false;
            // 
            // Label_QuestionToken
            // 
            resources.ApplyResources(this.Label_QuestionToken, "Label_QuestionToken");
            this.Label_QuestionToken.Name = "Label_QuestionToken";
            this.Label_QuestionToken.StringID_Text = "gj_label_questionchar";
            this.Label_QuestionToken.Click += new System.EventHandler(this.Label_QuestionToken_Click);
            this.Label_QuestionToken.MouseEnter += new System.EventHandler(this.Label_QuestionToken_MouseEnter);
            this.Label_QuestionToken.MouseLeave += new System.EventHandler(this.Label_QuestionToken_MouseLeave);
            // 
            // WatermarkTextBox_Username
            // 
            this.WatermarkTextBox_Username.Hint = "gj_wtextbox_username";
            resources.ApplyResources(this.WatermarkTextBox_Username, "WatermarkTextBox_Username");
            this.WatermarkTextBox_Username.Name = "WatermarkTextBox_Username";
            this.WatermarkTextBox_Username.StringID_Hint = "gj_wtextbox_username";
            // 
            // WatermarkTextBox_Token
            // 
            this.WatermarkTextBox_Token.Hint = "gj_wtextbox_token";
            resources.ApplyResources(this.WatermarkTextBox_Token, "WatermarkTextBox_Token");
            this.WatermarkTextBox_Token.Name = "WatermarkTextBox_Token";
            this.WatermarkTextBox_Token.StringID_Hint = "gj_wtextbox_token";
            // 
            // CheckBox_SaveCredentials
            // 
            resources.ApplyResources(this.CheckBox_SaveCredentials, "CheckBox_SaveCredentials");
            this.CheckBox_SaveCredentials.Name = "CheckBox_SaveCredentials";
            this.CheckBox_SaveCredentials.StringID_Text = "gj_checkbox_savecredentials";
            this.CheckBox_SaveCredentials.UseVisualStyleBackColor = true;
            this.CheckBox_SaveCredentials.CheckedChanged += new System.EventHandler(this.CheckBox_SaveCredentials_CheckedChanged);
            // 
            // CheckBox_AutoLogIn
            // 
            resources.ApplyResources(this.CheckBox_AutoLogIn, "CheckBox_AutoLogIn");
            this.CheckBox_AutoLogIn.Name = "CheckBox_AutoLogIn";
            this.CheckBox_AutoLogIn.StringID_Text = "gj_checkbox_autologin";
            this.CheckBox_AutoLogIn.UseVisualStyleBackColor = true;
            // 
            // GameJoltForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CheckBox_SaveCredentials);
            this.Controls.Add(this.CheckBox_AutoLogIn);
            this.Controls.Add(this.Label_QuestionToken);
            this.Controls.Add(this.WatermarkTextBox_Username);
            this.Controls.Add(this.WatermarkTextBox_Token);
            this.Controls.Add(this.PictureBox_GameJoltLogo);
            this.Controls.Add(this.Button_PlayOffline);
            this.Controls.Add(this.Button_SignIn);
            this.Controls.Add(this.Button_LogIn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GameJoltForm";
            this.StringID_Title = "gj_title";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_GameJoltLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private P3D.Legacy.Launcher.Controls.LocalizableButton Button_LogIn;
        private P3D.Legacy.Launcher.Controls.LocalizableButton Button_SignIn;
        private P3D.Legacy.Launcher.Controls.LocalizableButton Button_PlayOffline;
        private System.Windows.Forms.PictureBox PictureBox_GameJoltLogo;
        private P3D.Legacy.Launcher.Controls.LocalizableLabel Label_QuestionToken;
        private P3D.Legacy.Launcher.Controls.LocalizableCheckBox CheckBox_SaveCredentials;
        private P3D.Legacy.Launcher.Controls.LocalizableCheckBox CheckBox_AutoLogIn;
        private Controls.LocalizableWatermarkTextBox WatermarkTextBox_Token;
        private Controls.LocalizableWatermarkTextBox WatermarkTextBox_Username;
    }
}