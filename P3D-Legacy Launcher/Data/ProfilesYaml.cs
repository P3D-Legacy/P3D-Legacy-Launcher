using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using P3D.Legacy.Launcher.YamlConverters;
using P3D.Legacy.Launcher.Services;

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    internal class ProfilesYaml
    {
        private static ProfilesYaml Default => new ProfilesYaml { SelectedProfileIndex = 0, ProfileList = new List<ProfileYaml> { ProfileYaml.Default } };

        private static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().WithTypeConverter(new VersionConverter());
        private static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().WithTypeConverter(new VersionConverter());
        public static void Save(ProfilesYaml profiles)
        {
            if (!File.Exists(FileSystem.ProfilesFilePath))
                File.Create(FileSystem.ProfilesFilePath).Dispose();

            var serializer = SerializerBuilder.Build();
            File.WriteAllText(FileSystem.ProfilesFilePath, serializer.Serialize(profiles));
        }
        public static ProfilesYaml Load()
        {
            if (!File.Exists(FileSystem.ProfilesFilePath))
                File.Create(FileSystem.ProfilesFilePath).Dispose();

            var deserializer = DeserializerBuilder.Build();
            try
            {
                var deserialized = deserializer.Deserialize<ProfilesYaml>(File.ReadAllText(FileSystem.ProfilesFilePath));
                return deserialized != null && deserialized.IsValid() ? deserialized : Default;
            }
            catch (YamlException)
            {
                Save(Default);
                return deserializer.Deserialize<ProfilesYaml>(File.ReadAllText(FileSystem.ProfilesFilePath));
            }

        }

        public static IEnumerable<System.Version> AvailableVersions => LazyAvailableVersions.Value;
        private static Lazy<IEnumerable<System.Version>> LazyAvailableVersions { get; } = new Lazy<IEnumerable<System.Version>>(() => Task.Run(GetAvailableVersions).Result.OrderByDescending(version => version));
        private static async Task<IEnumerable<System.Version>> GetAvailableVersions()
        {
            if(!GitHub.WebsiteIsUp)
                return new List<System.Version>();

            try { return new List<System.Version>((await GitHub.GetAllGitHubReleases()).Select(release => release.Version)); }
            catch (Exception) { return new List<System.Version>(); }
        }

        public int SelectedProfileIndex { get; set; }

        public List<ProfileYaml> ProfileList { get; private set; } = new List<ProfileYaml>();

        public ProfileYaml GetProfile()
        {
            if (ProfileList.Count <= SelectedProfileIndex)
                SelectedProfileIndex = ProfileList.Count - 1;
            return ProfileList.Any() ? ProfileList[SelectedProfileIndex] : null;
        }

        public bool IsValid() => GetProfile() != null && GetProfile().Name != null && GetProfile().Version != null;
    }
    internal class ProfileYaml
    {
        public static ProfileYaml Default => new ProfileYaml { Name = "Latest", Version = ProfilesYaml.AvailableVersions.FirstOrDefault() ?? new System.Version("0.0") };

        public string Name { get; set; }
        public System.Version Version { get; set; }

        [YamlIgnore]
        public bool IsDefault
        {
            get
            {
                var latestVersion = ProfilesYaml.AvailableVersions.FirstOrDefault();
                return Name == "Latest" && latestVersion != null ? latestVersion == Version : Version == new System.Version("0.0");
            }
        }
        [YamlIgnore]
        public bool IsSupportingGameJolt => Version <= new System.Version("0.54.1");
    }
}