using P3D.Legacy.Launcher.Services;

namespace P3D.Legacy.Launcher.Controls
{
    internal class LocalizableWatermarkTextBox : WatermarkTextBox
    {
        private string stringID;
        public virtual string StringID_Hint
        {
            get { return stringID; }
            set { stringID = value; Hint = LocalizationUI.GetString(stringID);  }
        }
    }
}
