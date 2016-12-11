using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Launcher.Yaml;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    public class Profiles
    {
        public static IEnumerable<Version> AvailableVersions => GitHubInfo.GetAllReleases.Select(release => new Version(release.TagName));
        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().WithTypeConverter(new VersionConverter());
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().WithTypeConverter(new VersionConverter());

        public static Profiles Default => new Profiles { ProfileIndex = 0, ProfileList = new List<Profile> { Profile.Default } };


        public int ProfileIndex { get; set; } = 0;
        public List<Profile> ProfileList { get; private set; } = new List<Profile>();

        public Profile GetProfile() => ProfileList[ProfileIndex];
    }

    public class Profile
    {
        public static Profile Default => new Profile { Name = "Latest", Version = Profiles.AvailableVersions.FirstOrDefault() ?? new Version("0.0")  };

        public string Name { get; set; }
        public Version Version { get; set; }
    }
}
