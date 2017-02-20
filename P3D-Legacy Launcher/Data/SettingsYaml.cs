using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using P3D.Legacy.Launcher.YamlConverters;
using P3D.Legacy.Launcher.Services;
using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.YamlConverters;

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    internal class SettingsYaml
    {
        private static SettingsYaml Default => new SettingsYaml { GameUpdates = true, Language = new CultureInfo("en"), GameJoltUsername = "", GameJoltToken = "" };

        private static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().WithTypeConverter(new CultureInfoConverter()).WithTypeConverter(new UriConverter()).WithTypeConverter(new EncodedStringConverter());
        private static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().WithTypeConverter(new CultureInfoConverter()).WithTypeConverter(new UriConverter()).WithTypeConverter(new EncodedStringConverter());

        public static void Save(SettingsYaml settings)
        {
            if (!File.Exists(FileSystem.SettingsFilePath))
                File.Create(FileSystem.SettingsFilePath).Dispose();

            var serializer = SerializerBuilder.Build();
            File.WriteAllText(FileSystem.SettingsFilePath, serializer.Serialize(settings));
        }
        public static SettingsYaml Load()
        {
            if (!File.Exists(FileSystem.SettingsFilePath))
                File.Create(FileSystem.SettingsFilePath).Dispose();

            var deserializer = DeserializerBuilder.Build();
            try
            {
                var deserialized = deserializer.Deserialize<SettingsYaml>(File.ReadAllText(FileSystem.SettingsFilePath));
                return deserialized != null && deserialized.IsValid() ? deserialized : Default;
            }
            catch (YamlException)
            {
                Save(Default);
                return deserializer.Deserialize<SettingsYaml>(File.ReadAllText(FileSystem.SettingsFilePath)) ?? Default;
            }
        }

        public static CultureInfo[] AvailableCultureInfo { get; } = {
            new CultureInfo("en"), new CultureInfo("ru"), new CultureInfo("lt"),
            new CultureInfo("nl"), new CultureInfo("es"), new CultureInfo("de"),
            new CultureInfo("pl") 
            /* new CultureInfo("fr"), new CultureInfo("it"), new CultureInfo("pt") */
        };


        public bool GameUpdates { get; set; }
        public CultureInfo Language { get; set; }

        public EncodedString GameJoltUsername { get; set; } = "";
        public EncodedString GameJoltToken { get; set; } = "";
        public bool AutoLogIn { get; set; }
        public bool SaveCredentials { get; set; }

        public int SelectedDLIndex { get; set; }

        public static List<Uri> DLList => LazyDLList.Value;
        private static Lazy<List<Uri>> LazyDLList { get; } = new Lazy<List<Uri>>(() => Task.Run(GetDLUris).Result);
        private static async Task<List<Uri>> GetDLUris()
        {
            if(!GitHub.WebsiteIsUp)
                return new List<Uri>();

            try
            {
                var downloaded = await new WebClient().DownloadStringTaskAsync(new Uri("https://raw.githubusercontent.com/P3D-Legacy/P3D-Legacy-Data/master/DLUris.txt"));
                var strings = string.IsNullOrEmpty(downloaded) ? new string[0] : downloaded.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                return strings.All(string.IsNullOrEmpty) ? new List<Uri>() : strings.Select(str => new Uri(str)).ToList();
            }
            catch (WebException) { return new List<Uri>(); }
        }

        [YamlIgnore]
        public Uri SelectedDL => DLList.Any() ? DLList[SelectedDLIndex] : new Uri("https://dl.dropboxusercontent.com/u/58476180/P3D/");

        public bool IsValid() => Language != null /* && SelectedDL != null*/;
    }
}