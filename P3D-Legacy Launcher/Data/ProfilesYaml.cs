using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Launcher.YamlConverters;
using P3D.Legacy.Shared.Extensions;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    internal class ProfilesYaml
    {
        public static ProfilesYaml Default => new ProfilesYaml(0, new List<ProfileYaml> { ProfileYaml.Default });

        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().EmitDefaults().WithTypeConverter(new VersionConverter()).WithTypeConverter(new ProfileTypeConverter());
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().IgnoreUnmatchedProperties().WithTypeConverter(new VersionConverter()).WithTypeConverter(new ProfileTypeConverter());


        [YamlMember(Alias = "SelectedProfileIndex")]
        public int SelectedProfileIndex { get; private set; }
        [YamlMember(Alias = "ProfileList")]
        public List<ProfileYaml> ProfileList { get; private set; }

        public ProfilesYaml() { }
        public ProfilesYaml(int selectedProfileIndex, List<ProfileYaml> profileList) { SelectedProfileIndex = selectedProfileIndex; ProfileList = profileList; }

        public bool IsValid()
        {
            if (ProfileList == null)
                return false;

            if (!ProfileList.Any())
                return false;

            if (ProfileList.Count <= SelectedProfileIndex)
                return false;

            if (SelectedProfileIndex < 0)
                return false;

            return ProfileList[SelectedProfileIndex].Name != null && ProfileList[SelectedProfileIndex].Version != null;
        }
    }

    internal class ProfileYaml
    {
        public static ProfileYaml Default => new ProfileYaml(ProfileType.Game, "Latest", AsyncExtensions.RunSync(async () => await Profile.GetAvailableVersionsAsync(ProfileType.Game)).FirstOrDefault() ?? Profile.NoVersion, string.Empty);


        [YamlMember(Alias = "ProfileType")]
        public ProfileType ProfileType { get; private set; }

        [YamlMember(Alias = "Name")]
        public string Name { get; private set; }
        [YamlMember(Alias = "Version")]
        public Version Version { get; private set; }
        [YamlMember(Alias = "LaunchArgs")]
        public string LaunchArgs { get; private set; }

        public ProfileYaml() { }
        public ProfileYaml(ProfileType profileType, string name, Version version, string launchArgs) { ProfileType = profileType; Name = name; Version = version; LaunchArgs = launchArgs; }
    }
}