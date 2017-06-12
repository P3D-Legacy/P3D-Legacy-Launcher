using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using P3D.Legacy.Launcher.Controls;
using P3D.Legacy.Launcher.Services;
using P3D.Legacy.Launcher.Storage.Files;

namespace P3D.Legacy.Launcher.Forms
{
    internal partial class GameJoltForm : LocalizableForm
    {
        private SettingsFile Settings { get; }

        private ToolTip ToolTip_WatermarkTextBox_Token { get; } = new ToolTip();

        public GameJoltForm(SettingsFile settings)
        {
            Settings = settings;

            InitializeComponent();

            CheckBox_SaveCredentials.Checked = Settings.SaveCredentials;
            CheckBox_AutoLogIn.Checked = Settings.AutoLogIn;
            if (CheckBox_SaveCredentials.Checked)
            {
                WatermarkTextBox_Username.Text = Settings.GameJoltUsername;
                WatermarkTextBox_Token.Text = Settings.GameJoltToken;
            }
        }

        private async void Button_LogIn_Click(object sender, EventArgs e)
        {
            Settings.SaveCredentials = CheckBox_SaveCredentials.Checked;
            Settings.AutoLogIn = CheckBox_AutoLogIn.Checked;
            if (Settings.SaveCredentials)
            {
                Settings.GameJoltUsername = WatermarkTextBox_Username.Text;
                Settings.GameJoltToken = WatermarkTextBox_Token.Text;
            }
            await Settings.SaveAsync();

            Close();
            DialogResult = DialogResult.Yes;
        }
        private void Button_SignIn_Click(object sender, EventArgs e) => Process.Start(new ProcessStartInfo("http://gamejolt.com/join"));
        private void Button_PlayOffline_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Ignore;
        }

        private void Label_QuestionToken_MouseEnter(object sender, EventArgs e) => ToolTip_WatermarkTextBox_Token.Show(LocalizationUI.GetString("mf_label_token_hint"), Label_QuestionToken);
        private void Label_QuestionToken_MouseLeave(object sender, EventArgs e) => ToolTip_WatermarkTextBox_Token.Hide(Label_QuestionToken);
        private void Label_QuestionToken_Click(object sender, EventArgs e) => ToolTip_WatermarkTextBox_Token.Show(LocalizationUI.GetString("mf_label_token_hint"), Label_QuestionToken);

        private ToolTip ToolTip_SaveCredentials { get; } = new ToolTip();
        private void CheckBox_SaveCredentials_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_SaveCredentials.Checked)
            {
                CheckBox_AutoLogIn.ForeColor = DefaultForeColor;
                CheckBox_AutoLogIn.AutoCheck = true;

                ToolTip_SaveCredentials.RemoveAll();
            }
            else
            {
                CheckBox_AutoLogIn.Checked = false;
                CheckBox_AutoLogIn.ForeColor = Color.DimGray;
                CheckBox_AutoLogIn.AutoCheck = false;

                ToolTip_SaveCredentials.SetToolTip(CheckBox_AutoLogIn, LocalizationUI.GetString("ToolTipSaveCredentials"));
            }
        }
    }
}
