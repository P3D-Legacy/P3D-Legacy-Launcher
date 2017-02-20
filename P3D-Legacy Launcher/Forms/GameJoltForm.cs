using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using P3D.Legacy.Launcher.Data;

namespace P3D.Legacy.Launcher.Forms
{
    internal partial class GameJoltForm : Form
    {
        private ToolTip ToolTip_WatermarkTextBox_Token { get; } = new ToolTip();

        public GameJoltForm()
        {
            InitializeComponent();

            var settings = SettingsYaml.Load();
            WatermarkTextBox_Username.Text = settings.GameJoltUsername;
            WatermarkTextBox_Token.Text = settings.GameJoltToken;
            CheckBox_SaveCredentials.Checked = settings.SaveCredentials;
            CheckBox_AutoLogIn.Checked = settings.AutoLogIn;
        }

        private void Button_LogIn_Click(object sender, EventArgs e)
        {
            var settings = SettingsYaml.Load();
            settings.GameJoltUsername = WatermarkTextBox_Username.Text;
            settings.GameJoltToken = WatermarkTextBox_Token.Text;
            settings.SaveCredentials = CheckBox_SaveCredentials.Checked;
            settings.AutoLogIn = CheckBox_AutoLogIn.Checked;
            SettingsYaml.Save(settings);

            Close();
            DialogResult = DialogResult.Yes;
        }

        private void Button_SignIn_Click(object sender, EventArgs e) => Process.Start(new ProcessStartInfo("http://gamejolt.com/join"));

        private void Button_PlayOffline_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Ignore;
        }

        private void Label_QuestionToken_MouseEnter(object sender, EventArgs e) => ToolTip_WatermarkTextBox_Token.Show(MBLang.ToolTipGameJoltToken, Label_QuestionToken);
        private void Label_QuestionToken_MouseLeave(object sender, EventArgs e) => ToolTip_WatermarkTextBox_Token.Hide(Label_QuestionToken);
        private void Label_QuestionToken_Click(object sender, EventArgs e) => ToolTip_WatermarkTextBox_Token.Show(MBLang.ToolTipGameJoltToken, Label_QuestionToken);

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

                ToolTip_SaveCredentials.SetToolTip(CheckBox_AutoLogIn, MBLang.ToolTipSaveCredentials);
            }
        }
    }
}
