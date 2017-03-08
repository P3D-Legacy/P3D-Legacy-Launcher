using P3D.Legacy.Launcher.Services;

namespace P3D.Legacy.Launcher.Controls
{
    internal class LocalizableWatermarkTextBox : WatermarkTextBox
    {
        private string _stringID;
        public override string Hint
        {
            get { return LocalizationUI.GetString(_stringID); }
            set { _stringID = value; }
        }
    }
}
