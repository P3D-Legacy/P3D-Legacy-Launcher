using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace P3D.Legacy.Launcher.Data
{
    public class ProfileType : IEnumerable<ProfileType>
    {
        public static int GetIndex(ProfileType profileType) => ProfileTypes.IndexOf(profileType);
        public static ProfileType GetProfileType(int index) => ProfileTypes[index];
        public static object GetProfileType(string name) => ProfileTypes.SingleOrDefault(profileType => profileType.Name == name) ?? Game;

        public static ProfileType Game { get; } = new ProfileType("Pokémon3D", "Pokemon3D.exe", "", version => version >= new Version("0.55"), 0);
        public static ProfileType Server1 { get; } = new ProfileType("PokeD Server", "PokeD.Server.Desktop.exe", "-c", version => false, 1);
        public static ProfileType Server2 { get; } = new ProfileType("AGN Server", "Pokemon.3D.Server.Client.GUI.exe", "", version => false, 2);
        private static List<ProfileType> ProfileTypes { get; } = new List<ProfileType> { Game , Server1, Server2};


        public string Name { get; }
        public string Exe { get; }
        public string DefaultLaunchArgs { get; }
        private Func<Version, bool> IsSupportingGameJoltFunc { get; }
        private int Index { get; }

        private ProfileType(string name, string exe, string defaultLaunchArgs, Func<Version, bool> isSupportingGameJolt, int index)
        {
            Name = name;
            Exe = exe;
            DefaultLaunchArgs = defaultLaunchArgs;
            IsSupportingGameJoltFunc = isSupportingGameJolt;
            Index = index;
        }

        public bool IsSupportingGameJolt(Version version) => IsSupportingGameJoltFunc(version);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ProfileType> GetEnumerator() => ProfileTypes.GetEnumerator();

        public override string ToString() => Name;
    }
}