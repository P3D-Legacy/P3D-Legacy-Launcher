using System.Windows.Forms;

using P3D.Legacy.Launcher.Services;

namespace P3D.Legacy.Launcher.Controls
{
    internal class LocalizableTabPage : TabPage
    {
        private string _stringID;
        public override string Text
        {
            get { return LocalizationUI.GetString(_stringID); }
            set { _stringID = value; }
        }
    }
}
