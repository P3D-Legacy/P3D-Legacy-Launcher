using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using P3D.Legacy.Launcher.Services;
using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Extensions;

using PCLExt.FileStorage;

using YamlDotNet.Core;

namespace P3D.Legacy.Launcher.Data
{
    internal class Settings
    {
        private static SettingsYaml ToYaml(Settings settings) => new SettingsYaml(settings.GameUpdates, settings.LocalizationInfo, settings.GameJoltUsername, settings.GameJoltToken, settings.AutoLogIn, settings.SaveCredentials);
        private static Settings FromYaml(SettingsYaml yaml) => new Settings(yaml.GameUpdates, yaml.LocalizationInfo, yaml.GameJoltUsername, yaml.GameJoltToken, yaml.AutoLogIn, yaml.SaveCredentials);

        public static async Task SaveAsync(Settings settings)
        {
            var serializer = SettingsYaml.SerializerBuilder.Build();
            await StorageInfo.SettingsFile.WriteAllTextAsync(serializer.Serialize(settings.ToYaml()));
        }
        public static async Task<Settings> LoadAsync()
        {
            var deserializer = SettingsYaml.DeserializerBuilder.Build();
            try
            {
                var deserialized = deserializer.Deserialize<SettingsYaml>(await StorageInfo.SettingsFile.ReadAllTextAsync());
                if (deserialized != null && deserialized.IsValid())
                    return FromYaml(deserialized);

                var @default = FromYaml(SettingsYaml.Default);
                await SaveAsync(@default);
                return @default;
            }
            catch (YamlException e)
            {
                await SaveAsync(FromYaml(SettingsYaml.Default));
                return FromYaml(deserializer.Deserialize<SettingsYaml>(await StorageInfo.SettingsFile.ReadAllTextAsync()) ?? SettingsYaml.Default);
            }
        }

        public static List<Uri> DLList { get; } = AsyncExtensions.RunSync(async () => await GetDLUrisAsync());
        private static async Task<List<Uri>> GetDLUrisAsync()
        {
            if (!GitHub.WebsiteIsUp)
                return new List<Uri>();

            try
            {
                var downloaded = await new WebClient().DownloadStringTaskAsync(new Uri("https://raw.githubusercontent.com/P3D-Legacy/P3D-Legacy-Data/master/DLUris.txt"));
                var strings = string.IsNullOrEmpty(downloaded) ? new string[0] : downloaded.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                return strings.All(string.IsNullOrEmpty) ? new List<Uri>() : strings.Select(str => new Uri(str)).ToList();
            }
            catch (WebException) { return new List<Uri>(); }
        }


        public bool GameUpdates { get; set; }
        public LocalizationInfo LocalizationInfo { get; set; }
        public EncodedString GameJoltUsername { get; set; }
        public EncodedString GameJoltToken { get; set; }
        public bool AutoLogIn { get; set; }
        public bool SaveCredentials { get; set; }
        public int SelectedDLIndex { get; set; }
        public Uri SelectedDL => DLList.Any() ? DLList[SelectedDLIndex] : new Uri("https://dl.dropboxusercontent.com/u/58476180/P3D/");

        private Settings(bool gameUpdates, LocalizationInfo localizationInfo, EncodedString gameJoltUsername, EncodedString gameJoltToken, bool autoLogIn, bool saveCredentials)
        {
            GameUpdates = gameUpdates;
            LocalizationInfo = localizationInfo;
            GameJoltUsername = gameJoltUsername;
            GameJoltToken = gameJoltToken;
            AutoLogIn = autoLogIn;
            SaveCredentials = saveCredentials;
        }

        public SettingsYaml ToYaml() => ToYaml(this);
        public async Task SaveAsync() => await SaveAsync(this);
        public async Task ReloadAsync()
        {
            var settings = await LoadAsync();

            GameUpdates = settings.GameUpdates;
            LocalizationInfo = settings.LocalizationInfo;
            GameJoltUsername = settings.GameJoltUsername;
            GameJoltToken = settings.GameJoltToken;
            AutoLogIn = settings.AutoLogIn;
            SaveCredentials = settings.SaveCredentials;
            SelectedDLIndex = settings.SelectedDLIndex;
        }
    }
}