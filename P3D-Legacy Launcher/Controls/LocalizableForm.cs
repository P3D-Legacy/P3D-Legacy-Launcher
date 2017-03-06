using System.Windows.Forms;

using P3D.Legacy.Launcher.Services;

namespace P3D.Legacy.Launcher.Controls
{
    internal class LocalizableForm : Form
    {
        private string _stringID;
        public virtual string StringID_Title
        {
            get { return _stringID; }
            set { _stringID = value; Text = LocalizationUI.GetString(_stringID); }
        }
    }
}
