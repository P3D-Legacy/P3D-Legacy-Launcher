using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using P3D.Legacy.Launcher.Forms;

namespace P3D.Legacy.Launcher
{
    public static class Program
    {
        public static List<Action> ActionsBeforeExit { get; } = new List<Action>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {

            //var ci = new CultureInfo("en");
            //var ci = new CultureInfo("ru");
            //var ci = new CultureInfo("lt");
            //var ci = new CultureInfo("nl");
            //var ci = new CultureInfo("es");
            //var ci = new CultureInfo("fr");
            //var ci = new CultureInfo("pl");
            //var ci = new CultureInfo("it");
            //var ci = new CultureInfo("pt");
            //Thread.CurrentThread.CurrentCulture = ci;
            //Thread.CurrentThread.CurrentUICulture = ci;
            //CultureInfo.DefaultThreadCurrentCulture = ci;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            if(ActionsBeforeExit.Any())
                foreach (var action in ActionsBeforeExit)
                    action();
            Environment.Exit(0);
        }
    }
}
