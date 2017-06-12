using System.Globalization;

using P3D.Legacy.Launcher.Storage.Files;
using P3D.Legacy.Launcher.YamlConverters;
using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.YamlConverters;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    internal class SettingsYaml : IYamlSettings
    {
        public static SettingsYaml Default => new SettingsYaml();

        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().EmitDefaults().WithTypeConverter(new CultureInfoConverter()).WithTypeConverter(new UriConverter()).WithTypeConverter(new EncodedStringConverter());
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().IgnoreUnmatchedProperties().WithTypeConverter(new CultureInfoConverter()).WithTypeConverter(new UriConverter()).WithTypeConverter(new EncodedStringConverter());

        [YamlMember(Alias = "GameUpdates")]
        public bool GameUpdates { get; private set; } = true;

        [YamlMember(Alias = "GameCheckServer")]
        public bool GameCheckServer { get; private set; } = true;

        [YamlMember(Alias = "LocalizationInfo")]
        public LocalizationInfo LocalizationInfo { get; private set; } = new LocalizationInfo(new CultureInfo("en"));

        [YamlMember(Alias = "GameJoltUsername")]
        public EncodedString GameJoltUsername { get; private set; } = "";
        [YamlMember(Alias = "GameJoltToken")]
        public EncodedString GameJoltToken { get; private set; } = "";
        [YamlMember(Alias = "AutoLogIn")]
        public bool AutoLogIn { get; private set; } = false;
        [YamlMember(Alias = "SaveCredentials")]
        public bool SaveCredentials { get; private set; } = false;

        //public int SelectedDLIndex { get; private set; }

        public SettingsYaml() { }
        public SettingsYaml(bool gameUpdates, bool gameCheckServer, LocalizationInfo localizationInfo, EncodedString gameJoltUsername, EncodedString gameJoltToken, bool autoLogIn, bool saveCredentials)
        {
            GameUpdates = gameUpdates;
            GameCheckServer = gameCheckServer;
            LocalizationInfo = localizationInfo;
            GameJoltUsername = gameJoltUsername;
            GameJoltToken = gameJoltToken;
            AutoLogIn = autoLogIn;
            SaveCredentials = saveCredentials;
        }

        public bool IsValid() => LocalizationInfo.CultureInfo != null /* && SelectedDL != null*/;
    }
}