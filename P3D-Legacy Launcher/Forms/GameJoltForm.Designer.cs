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
            this.Button_LogIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_LogIn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button_LogIn.Location = new System.Drawing.Point(12, 190);
            this.Button_LogIn.Name = "Button_LogIn";
            this.Button_LogIn.Size = new System.Drawing.Size(100, 23);
            this.Button_LogIn.TabIndex = 5;
            this.Button_LogIn.Text = "gj_button_login";
            this.Button_LogIn.UseVisualStyleBackColor = true;
            this.Button_LogIn.Click += new System.EventHandler(this.Button_LogIn_Click);
            // 
            // Button_SignIn
            // 
            this.Button_SignIn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Button_SignIn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button_SignIn.Location = new System.Drawing.Point(132, 190);
            this.Button_SignIn.Name = "Button_SignIn";
            this.Button_SignIn.Size = new System.Drawing.Size(120, 23);
            this.Button_SignIn.TabIndex = 6;
            this.Button_SignIn.Text = "gj_button_signin";
            this.Button_SignIn.UseVisualStyleBackColor = true;
            this.Button_SignIn.Click += new System.EventHandler(this.Button_SignIn_Click);
            // 
            // Button_PlayOffline
            // 
            this.Button_PlayOffline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_PlayOffline.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button_PlayOffline.Location = new System.Drawing.Point(272, 190);
            this.Button_PlayOffline.Name = "Button_PlayOffline";
            this.Button_PlayOffline.Size = new System.Drawing.Size(100, 23);
            this.Button_PlayOffline.TabIndex = 7;
            this.Button_PlayOffline.Text = "gj_button_playoffline";
            this.Button_PlayOffline.UseVisualStyleBackColor = true;
            this.Button_PlayOffline.Click += new System.EventHandler(this.Button_PlayOffline_Click);
            // 
            // PictureBox_GameJoltLogo
            // 
            this.PictureBox_GameJoltLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PictureBox_GameJoltLogo.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox_GameJoltLogo.Image")));
            this.PictureBox_GameJoltLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PictureBox_GameJoltLogo.Location = new System.Drawing.Point(0, 0);
            this.PictureBox_GameJoltLogo.Name = "PictureBox_GameJoltLogo";
            this.PictureBox_GameJoltLogo.Size = new System.Drawing.Size(384, 38);
            this.PictureBox_GameJoltLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PictureBox_GameJoltLogo.TabIndex = 4;
            this.PictureBox_GameJoltLogo.TabStop = false;
            // 
            // Label_QuestionToken
            // 
            this.Label_QuestionToken.AutoSize = true;
            this.Label_QuestionToken.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label_QuestionToken.Location = new System.Drawing.Point(350, 100);
            this.Label_QuestionToken.Name = "Label_QuestionToken";
            this.Label_QuestionToken.Size = new System.Drawing.Size(110, 13);
            this.Label_QuestionToken.TabIndex = 2;
            this.Label_QuestionToken.Text = "gj_label_questionchar";
            this.Label_QuestionToken.Click += new System.EventHandler(this.Label_QuestionToken_Click);
            this.Label_QuestionToken.MouseEnter += new System.EventHandler(this.Label_QuestionToken_MouseEnter);
            this.Label_QuestionToken.MouseLeave += new System.EventHandler(this.Label_QuestionToken_MouseLeave);
            // 
            // WatermarkTextBox_Username
            // 
            this.WatermarkTextBox_Username.Hint = "gj_wtextbox_username";
            this.WatermarkTextBox_Username.Location = new System.Drawing.Point(12, 71);
            this.WatermarkTextBox_Username.Name = "WatermarkTextBox_Username";
            this.WatermarkTextBox_Username.Size = new System.Drawing.Size(360, 20);
            this.WatermarkTextBox_Username.TabIndex = 0;
            // 
            // WatermarkTextBox_Token
            // 
            this.WatermarkTextBox_Token.Hint = "gj_wtextbox_token";
            this.WatermarkTextBox_Token.Location = new System.Drawing.Point(12, 97);
            this.WatermarkTextBox_Token.Name = "WatermarkTextBox_Token";
            this.WatermarkTextBox_Token.PasswordChar = '*';
            this.WatermarkTextBox_Token.Size = new System.Drawing.Size(360, 20);
            this.WatermarkTextBox_Token.TabIndex = 1;
            // 
            // CheckBox_SaveCredentials
            // 
            this.CheckBox_SaveCredentials.AutoSize = true;
            this.CheckBox_SaveCredentials.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CheckBox_SaveCredentials.Location = new System.Drawing.Point(12, 123);
            this.CheckBox_SaveCredentials.Name = "CheckBox_SaveCredentials";
            this.CheckBox_SaveCredentials.Size = new System.Drawing.Size(167, 17);
            this.CheckBox_SaveCredentials.TabIndex = 3;
            this.CheckBox_SaveCredentials.Text = "gj_checkbox_savecredentials";
            this.CheckBox_SaveCredentials.UseVisualStyleBackColor = true;
            this.CheckBox_SaveCredentials.CheckedChanged += new System.EventHandler(this.CheckBox_SaveCredentials_CheckedChanged);
            // 
            // CheckBox_AutoLogIn
            // 
            this.CheckBox_AutoLogIn.AutoSize = true;
            this.CheckBox_AutoLogIn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CheckBox_AutoLogIn.Location = new System.Drawing.Point(12, 146);
            this.CheckBox_AutoLogIn.Name = "CheckBox_AutoLogIn";
            this.CheckBox_AutoLogIn.Size = new System.Drawing.Size(136, 17);
            this.CheckBox_AutoLogIn.TabIndex = 4;
            this.CheckBox_AutoLogIn.Text = "gj_checkbox_autologin";
            this.CheckBox_AutoLogIn.UseVisualStyleBackColor = true;
            // 
            // GameJoltForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 225);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 180);
            this.Name = "GameJoltForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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