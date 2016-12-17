using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Launcher.Converters;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    public class ProfilesYaml
    {
        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().WithTypeConverter(new VersionConverter());
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().WithTypeConverter(new VersionConverter());

        public static IEnumerable<Version> AvailableVersions => GitHubInfo.GetAllReleases.Select(release => new Version(release.TagName));

        public static ProfilesYaml Default => new ProfilesYaml { SelectedProfileIndex = 0, ProfileList = new List<ProfileYaml> { ProfileYaml.Default } };


        private int _selectedProfileIndex;
        public int SelectedProfileIndex { get { return _selectedProfileIndex; } set { if(value > 0 || value < ProfileList.Count) _selectedProfileIndex = value; } }
        public List<ProfileYaml> ProfileList { get; private set; }

        public ProfileYaml GetProfile() => ProfileList.Any() ? ProfileList[SelectedProfileIndex] : null;
        public bool IsValid() => GetProfile() != null && GetProfile().Name != null && GetProfile().Version != null;
    }
    public class ProfileYaml
    {
        public static ProfileYaml Default => new ProfileYaml { Name = "Latest", Version = ProfilesYaml.AvailableVersions.FirstOrDefault() ?? new Version("0.0") };

        public string Name { get; set; }
        public Version Version { get; set; }
    }
}