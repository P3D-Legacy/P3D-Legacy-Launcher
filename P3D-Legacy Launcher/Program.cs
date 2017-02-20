using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using P3D.Legacy.Launcher.Forms;

namespace P3D.Legacy.Launcher
{
    public static class Program
    {
        public static List<Action> ActionsBeforeExit { get; } = new List<Action>();

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            if(ActionsBeforeExit.Any())
                foreach (var action in ActionsBeforeExit)
                    action?.Invoke();
            Environment.Exit(0);
        }
    }
}
