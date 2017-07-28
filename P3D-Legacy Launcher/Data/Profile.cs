using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using P3D.Legacy.Launcher.Services;
using P3D.Legacy.Launcher.Storage.Files;
using P3D.Legacy.Launcher.Storage.Folders;
using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Extensions;

namespace P3D.Legacy.Launcher.Data
{
    internal sealed class Profile
    {
        public static Version NoVersion { get; } = new Version("0.0");

        public static bool operator ==(Profile a, Profile b) => ReferenceEquals(a, null) ? ReferenceEquals(b, null) : a.Equals((object)b);
        public static bool operator !=(Profile a, Profile b) => !(a == b);

        public static async Task<List<Version>> GetAvailableVersionsAsync(ProfileType profileType)
        {
            if (!GitHub.WebsiteIsUp)
                return new List<Version>();

            try { return (await GitHub.GetAllReleasesAsync(profileType)).Select(release => release.Version).OrderByDescending(version => version).ToList(); }
            catch (Exception) { return new List<Version>(); }
        }

        public static async Task DeleteAsync(Profile profile) => await profile.Folder.DeleteAsync();

        public static ProfileYaml ToYaml(Profile profile) => new ProfileYaml(profile.ProfileType, profile.Name, profile.Version, profile.LaunchArgs);
        public static Profile FromYaml(ProfileYaml yaml) => new Profile(yaml.ProfileType, yaml.Name, yaml.Version, yaml.LaunchArgs);


        public ProfileType ProfileType { get; }
        public string Name { get; }
        public Version Version { get; }
        public Version VersionExe => ExecutionFile != null ? new Version(FileVersionInfo.GetVersionInfo(ExecutionFile.Path).ProductVersion) : NoVersion;
        public string LaunchArgs { get; }

        public ProfileExeFile ExecutionFile => Folder.ExecutionFile;
        public ProfileFolder Folder => new ProfileFolder(Name, ProfileType);
        public List<ModificationInfo> ModificationInfos => Folder.ModFolder.GetModificationFolders().Select(mod => mod.ModificationFile.ModificationInfo).ToList();

        public bool IsDefault => Name == "Latest";
        public bool IsSupportingGameJolt => ProfileType.IsSupportingGameJolt(Version);

        public Profile(ProfileType profileType, string name, Version version, string launchArgs = "")
        {
            ProfileType = profileType ?? ProfileType.Game;
            Name = name;
            Version = version;
            LaunchArgs = string.IsNullOrEmpty(launchArgs) ? ProfileType.DefaultLaunchArgs : launchArgs;

            Version[] versions;
            if (Equals(Version, NoVersion) && (versions = AsyncExtensions.RunSync(async () => await GetAvailableVersionsAsync()).ToArray()).Length > 0)
                Version = versions.First();
        }

        public async Task<List<GitHubRelease>> GetAvailableReleasesAsync() => await GitHub.GetAllReleasesAsync(ProfileType);
        public async Task<List<Version>> GetAvailableVersionsAsync() => await GetAvailableVersionsAsync(ProfileType);

        public ProfileYaml ToYaml() => ToYaml(this);
        public async Task DeleteAsync() => await DeleteAsync(this);

        private bool Equals(Profile other) => string.Equals(Name, other.Name);
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Profile)obj);
        }
        public override int GetHashCode() => Name?.GetHashCode() ?? 0;

        public override string ToString() => Name;
    }
}
