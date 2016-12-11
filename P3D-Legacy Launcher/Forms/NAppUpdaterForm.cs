using System.Windows.Forms;

using P3D.Legacy.Launcher.Data;

namespace P3D.Legacy.Launcher.Forms
{
    public partial class NAppUpdaterForm : Form
    {
        private OnlineGameRelease onlineRelease;

        public NAppUpdaterForm()
        {
            InitializeComponent();
        }

        public NAppUpdaterForm(OnlineGameRelease onlineRelease)
        {
            this.onlineRelease = onlineRelease;
        }
    }
}
