using System.Globalization;

using P3D.Legacy.Launcher.YamlConverters;
using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.YamlConverters;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    internal class SettingsYaml
    {
        public static SettingsYaml Default => new SettingsYaml(true, new LocalizationInfo(new CultureInfo("en")), "", "", false, false);

        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().EmitDefaults().WithTypeConverter(new CultureInfoConverter()).WithTypeConverter(new UriConverter()).WithTypeConverter(new EncodedStringConverter());
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().IgnoreUnmatchedProperties().WithTypeConverter(new CultureInfoConverter()).WithTypeConverter(new UriConverter()).WithTypeConverter(new EncodedStringConverter());

        [YamlMember(Alias = "GameUpdates")]
        public bool GameUpdates { get; private set; }

        [YamlMember(Alias = "LocalizationInfo")]
        public LocalizationInfo LocalizationInfo { get; private set; }

        [YamlMember(Alias = "GameJoltUsername")]
        public EncodedString GameJoltUsername { get; private set; }
        [YamlMember(Alias = "GameJoltToken")]
        public EncodedString GameJoltToken { get; private set; }
        [YamlMember(Alias = "AutoLogIn")]
        public bool AutoLogIn { get; private set; }
        [YamlMember(Alias = "SaveCredentials")]
        public bool SaveCredentials { get; private set; }

        //public int SelectedDLIndex { get; private set; }

        public SettingsYaml() { }
        public SettingsYaml(bool gameUpdates, LocalizationInfo localizationInfo, EncodedString gameJoltUsername, EncodedString gameJoltToken, bool autoLogIn, bool saveCredentials)
        {
            GameUpdates = gameUpdates;
            LocalizationInfo = localizationInfo;
            GameJoltUsername = gameJoltUsername;
            GameJoltToken = gameJoltToken;
            AutoLogIn = autoLogIn;
            SaveCredentials = saveCredentials;
        }

        public bool IsValid() => LocalizationInfo.CultureInfo != null /* && SelectedDL != null*/;
    }
}