using System.Diagnostics;
using System.Linq;

using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Utils;

namespace P3D.Legacy.Launcher.Services
{
    internal static class LocalizationUI
    {
        private static Localization Localization { get; set; }

        public static LocalizationInfo[] Localizations => Localization.Localizations;
        public static string[] Authors => Localization.AllAuthors.ToArray();

        public static void Load(LocalizationInfo localizationInfo) => Localization = new Localization(localizationInfo);

        [DebuggerStepThrough]
        public static string GetString(string stringID) => Localization?.GetString(stringID) ?? stringID;
        [DebuggerStepThrough]
        public static string GetString(string stringID, params object[] args) => string.Format(Localization?.GetString(stringID) ?? stringID, args);
    }
}
