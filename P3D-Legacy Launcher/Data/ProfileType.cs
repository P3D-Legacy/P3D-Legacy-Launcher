using System;
using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Launcher.Storage.Folders;

namespace P3D.Legacy.Launcher.Data
{
    internal class ProfileType
    {
        private static IList<ProfileType> ProfileTypes { get; } = new List<ProfileType>();
        public static ProfileType[] ToArray() => ProfileTypes.ToArray();

        public static int GetIndex(ProfileType profileType) => ProfileTypes.IndexOf(profileType);
        public static ProfileType GetProfileType(int index) => ProfileTypes[index];
        public static object GetProfileType(string name) => ProfileTypes.SingleOrDefault(profileType => profileType.Name == name) ?? Game;

        public static ProfileType Game { get; } = new ProfileType("Pokémon3D", "Pokemon3D.exe", "GameModes", "", version => version >= new Version("0.55"));
        public static ProfileType Server1 { get; } = new ProfileType("PokeD Server", "PokeD.Server.Desktop.exe", "Plugins", "-cn", version => false);
        public static ProfileType Server2 { get; } = new ProfileType("AGN Server", "Pokemon.3D.Server.Client.GUI.exe", "Plugins", "", version => false);
       

        public string Name { get; }
        public string Exe { get; }
        public string ModFolder { get; }
        public string DefaultLaunchArgs { get; }
        private Func<Version, bool> IsSupportingGameJoltFunc { get; }

        private ProfileType(string name, string exe, string modFolder, string defaultLaunchArgs, Func<Version, bool> isSupportingGameJolt)
        {
            var t = new ProfileFolder(name, this);
            IList<ProfileType> y = new ProfileType[0];

            Name = name;
            Exe = exe;
            ModFolder = modFolder;
            DefaultLaunchArgs = defaultLaunchArgs;
            IsSupportingGameJoltFunc = isSupportingGameJolt;

            ProfileTypes.Add(this);
        }

        public bool IsSupportingGameJolt(Version version) => IsSupportingGameJoltFunc(version);

        public override string ToString() => Name;
    }
}