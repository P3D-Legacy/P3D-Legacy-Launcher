using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Launcher.Yaml;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    public class Profiles
    {
        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().WithTypeConverter(new VersionConverter());
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().WithTypeConverter(new VersionConverter());

        public static IEnumerable<Version> AvailableVersions => GitHubInfo.GetAllReleases.Select(release => new Version(release.TagName));

        public static Profiles Default => new Profiles { SelectedProfileIndex = 0, ProfileList = new List<Profile> { Profile.Default } };


        private int _selectedProfileIndex;
        public int SelectedProfileIndex { get { return _selectedProfileIndex; } set { if(value > 0 || value < ProfileList.Count) _selectedProfileIndex = value; } }
        public List<Profile> ProfileList { get; private set; }

        public Profile GetProfile() => ProfileList.Any() ? ProfileList[SelectedProfileIndex] : null;
        public bool IsValid() => GetProfile() != null && GetProfile().Name != null && GetProfile().Version != null;
    }

    public class Profile
    {
        public static Profile Default => new Profile { Name = "Latest", Version = Profiles.AvailableVersions.FirstOrDefault() ?? new Version("0.0")  };

        public string Name { get; set; }
        public Version Version { get; set; }
    }
}
