using System;
using System.Windows.Forms;

namespace P3D.Legacy.Launcher.Extensions
{
    internal static class ControlExtensions
    {
        public static void SafeInvoke(this Control uiElement, Action updater, bool forceSynchronous = false)
        {
            if (uiElement == null)
                return;

            if (uiElement.InvokeRequired)
            {
                if (forceSynchronous)
                    uiElement.Invoke((Action) delegate { SafeInvoke(uiElement, updater, true); });
                else
                    uiElement.BeginInvoke((Action) delegate { SafeInvoke(uiElement, updater, false); });
            }
            else
            {
                if (uiElement.IsDisposed)
                    return;

                updater();
            }
        }
    }
}
