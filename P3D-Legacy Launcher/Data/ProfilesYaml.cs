using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Launcher.Storage.Files;
using P3D.Legacy.Launcher.YamlConverters;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Data
{
    internal class ProfilesYaml : IYamlSettings
    {
        public static ProfilesYaml Default => new ProfilesYaml();

        public static SerializerBuilder SerializerBuilder { get; } = new SerializerBuilder().EmitDefaults().WithTypeConverter(new VersionConverter()).WithTypeConverter(new ProfileTypeConverter());
        public static DeserializerBuilder DeserializerBuilder { get; } = new DeserializerBuilder().IgnoreUnmatchedProperties().WithTypeConverter(new VersionConverter()).WithTypeConverter(new ProfileTypeConverter());


        [YamlMember(Alias = "SelectedProfileIndex")]
        public int SelectedProfileIndex { get; private set; } = 0;
        [YamlMember(Alias = "ProfileList")]
        public List<ProfileYaml> ProfileList { get; private set; } = new List<ProfileYaml>();

        public ProfilesYaml() { }
        public ProfilesYaml(int selectedProfileIndex, List<ProfileYaml> profileList)
        {
            SelectedProfileIndex = selectedProfileIndex;
            ProfileList = profileList;
        }

        public bool IsValid()
        {
            if (SelectedProfileIndex < 0)
                SelectedProfileIndex = 0;
            if (SelectedProfileIndex - 1 >= ProfileList.Count)
                SelectedProfileIndex = ProfileList.Count;

            var index = SelectedProfileIndex > 0 ? SelectedProfileIndex - 1 : SelectedProfileIndex; // Because of Latest

            if (ProfileList == null)
                return false;

            if (!ProfileList.Any())
                return false;

            if (ProfileList.Count <= index)
                return false;

            if (SelectedProfileIndex < 0)
                return false;

            return ProfileList[index].Name != null && ProfileList[index].Version != null;
        }
    }

    internal class ProfileYaml
    {
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