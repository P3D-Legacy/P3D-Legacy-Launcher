using System.Linq;

using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Extensions;

namespace P3D.Legacy.Launcher.Services
{
    internal static class LocalizationUI
    {
        private static Localization Localization { get; set; }

        public static LocalizationInfo[] Localizations => AsyncExtensions.RunSync(async () => (await StorageInfo.LocalizationFolder.GetTranslationFilesAsync()).Select(tf => tf.LocalizationInfo).ToArray());

        public static void Load(LocalizationInfo localizationInfo) => Localization = new Localization(StorageInfo.LocalizationFolder, localizationInfo);

        public static string GetString(string stringID) => Localization?.GetString(stringID) ?? stringID;
        public static string GetString(string stringID, params object[] args) => string.Format(Localization?.GetString(stringID) ?? stringID, args);
    }
}
