using System;
using System.Windows.Forms;

using P3D.Legacy.Launcher.Forms;

namespace P3D.Legacy.Launcher
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            //var ci = new CultureInfo("en");
            //var ci = new CultureInfo("ru");
            //var ci = new CultureInfo("lt");
            //Thread.CurrentThread.CurrentCulture = ci;
            //Thread.CurrentThread.CurrentUICulture = ci;
            //CultureInfo.DefaultThreadCurrentCulture = ci;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
