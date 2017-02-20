using System.Drawing;
using System.Windows.Forms;

namespace P3D.Legacy.Launcher.Controls
{
    internal class WatermarkTextBox : TextBox
    {
        private string _hint;
        public string Hint
        {
            get { return _hint; }
            set { _hint = value; Invalidate(); }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0xf)
                if (!Focused && string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(Hint))
                    using (var graphics = CreateGraphics())
                        TextRenderer.DrawText(graphics, Hint, Font, ClientRectangle, SystemColors.GrayText, BackColor, TextFormatFlags.Top | TextFormatFlags.Left);
        }
    }
}
