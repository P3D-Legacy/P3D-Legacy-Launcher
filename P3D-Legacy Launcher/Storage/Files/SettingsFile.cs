using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using P3D.Legacy.Launcher.Data;
using P3D.Legacy.Launcher.Services;
using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Extensions;
using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Storage.Files
{
    internal sealed class SettingsFile : BaseYamlSettingsFile<SettingsYaml>
    {
        protected override SerializerBuilder SerializerBuilder { get; } = SettingsYaml.SerializerBuilder;
        protected override DeserializerBuilder DeserializerBuilder { get; } = SettingsYaml.DeserializerBuilder;
        protected override SettingsYaml Default { get; } = SettingsYaml.Default;

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
        public bool GameCheckServer { get; set; }
        public LocalizationInfo LocalizationInfo { get; set; }
        public EncodedString GameJoltUsername { get; set; }
        public EncodedString GameJoltToken { get; set; }
        public bool AutoLogIn { get; set; }
        public bool SaveCredentials { get; set; }
        public int SelectedDLIndex { get; set; }
        public Uri SelectedDL => DLList.Any() ? DLList[SelectedDLIndex] : new Uri("https://dl.dropboxusercontent.com/u/58476180/P3D/");


        public SettingsFile() : base(new MainFolder().CreateFile("Settings.yml", CreationCollisionOption.OpenIfExists)) { }

        protected override SettingsYaml ToYaml() => new SettingsYaml(GameUpdates, GameCheckServer, LocalizationInfo, GameJoltUsername, GameJoltToken, AutoLogIn, SaveCredentials);
        protected override void FromYaml(SettingsYaml yaml)
        {
            GameUpdates = yaml.GameUpdates;
            GameCheckServer = yaml.GameCheckServer;
            LocalizationInfo = yaml.LocalizationInfo;
            GameJoltUsername = yaml.GameJoltUsername;
            GameJoltToken = yaml.GameJoltToken;
            AutoLogIn = yaml.AutoLogIn;
            SaveCredentials = yaml.SaveCredentials;
        }
    }
}