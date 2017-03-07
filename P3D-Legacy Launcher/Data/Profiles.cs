using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using P3D.Legacy.Launcher.Services;
using P3D.Legacy.Shared.Extensions;

using PCLExt.FileStorage;

using YamlDotNet.Core;

namespace P3D.Legacy.Launcher.Data
{
    internal sealed class Profiles : IEnumerable<Profile>
    {
        internal enum SelectedProfile { Current, First, Last }

        public static ProfilesYaml ToYaml(Profiles profile) => new ProfilesYaml(profile.SelectedProfileIndex, profile.ProfileList.Select(Profile.ToYaml).ToList());
        public static Profiles FromYaml(ProfilesYaml profile) => new Profiles(profile.SelectedProfileIndex, profile.ProfileList.Select(Profile.FromYaml).ToList());

        public static async Task SaveAsync(ProfilesYaml yaml)
        {
            var serializer = ProfilesYaml.SerializerBuilder.Build();
            await StorageInfo.ProfilesFile.WriteAllTextAsync(serializer.Serialize(yaml));
        }
        public static async Task<Profiles> LoadAsync()
        {
            var deserializer = ProfilesYaml.DeserializerBuilder.Build();
            try
            {
                var deserialized = deserializer.Deserialize<ProfilesYaml>(await StorageInfo.ProfilesFile.ReadAllTextAsync());
                if (deserialized != null && deserialized.IsValid())
                    return FromYaml(deserialized);

                var @default = ProfilesYaml.Default;
                await SaveAsync(@default);
                return FromYaml(@default);
            }
            catch (YamlException)
            {
                await SaveAsync(ProfilesYaml.Default);
                return FromYaml(deserializer.Deserialize<ProfilesYaml>(await StorageInfo.ProfilesFile.ReadAllTextAsync()));
            }

        }


        private List<Profile> ProfileList { get; set; }
        public int SelectedProfileIndex { get; set; }
        public Profile CurrentProfile
        {
            get
            {
                if (ProfileList.Count <= SelectedProfileIndex)
                    SelectedProfileIndex = ProfileList.Count - 1;
                return ProfileList.Any() ? ProfileList[SelectedProfileIndex] : null;
            }
        }

        private Profiles(int selectedProfileIndex, List<Profile> profileList) { SelectedProfileIndex = selectedProfileIndex; ProfileList = profileList; }

        public async Task CreateAsync(ProfileType profileType, string name, System.Version version, string launchArgs = "")
        {
            ProfileList.Add(new Profile(profileType, name, version, launchArgs));
            await SaveAsync();
        }
        public async Task CreateAsync(Profile profile)
        {
            ProfileList.Add(profile);
            await SaveAsync();
        }
        public async Task DeleteAsync(int index)
        {
            if (ProfileList.Count > index && !ProfileList[index].IsDefault)
            {
                await ProfileList[index].Delete();
                ProfileList.RemoveAt(index);
            }
            await SaveAsync();
        }
        public async Task DeleteAsync(Profile profile)
        {
            if (ProfileList.Contains(profile) && !profile.IsDefault)
            {
                await profile.Delete();
                ProfileList.Remove(profile);
            }
            await SaveAsync();
        }

        public async Task ReplaceAsync(Profile profileOld, Profile profileNew)
        {
            for (var i = 0; i < ProfileList.Count; i++)
                if (ProfileList[i] == profileOld)
                    ProfileList[i] = profileNew;

            if (profileOld.Name != profileNew.Name)
            {
                var oldFolder = profileOld.Folder;
                var newFolder = profileNew.Folder;
                if ((await oldFolder.GetFilesAsync()).Any() || (await oldFolder.GetFoldersAsync()).Any())
                {
                    var folderReplace = await GetEmptyFolderRecursiveAsync(newFolder);

                    if ((await newFolder.GetFilesAsync()).Any() || (await newFolder.GetFoldersAsync()).Any())
                        await newFolder.MoveAsync(folderReplace);

                    await oldFolder.MoveAsync(newFolder);
                }
            }
        }
        private async Task<IFolder> GetEmptyFolderRecursiveAsync(IFolder folder)
        {
            if ((await folder.GetFilesAsync()).Any() || (await folder.GetFoldersAsync()).Any())
                return await GetEmptyFolderRecursiveAsync(await folder.CreateFolderAsync($"{folder.Name}_old", CreationCollisionOption.OpenIfExists));

            return folder;
        }

        public ProfilesYaml ToYaml() => ToYaml(this);
        public async Task SaveAsync()
        {
            if (ProfileList.Count <= SelectedProfileIndex)
                SelectedProfileIndex = 0;

            await SaveAsync(ToYaml());
        }
        public async Task ReloadAsync(SelectedProfile selectedProfile = SelectedProfile.Current)
        {
            var profiles = await LoadAsync();
            ProfileList = profiles.ProfileList;

            switch (selectedProfile)
            {
                case SelectedProfile.Current:
                    SelectedProfileIndex = SelectedProfileIndex;
                    break;
                case SelectedProfile.First:
                    SelectedProfileIndex = this.Any() ? 0 : SelectedProfileIndex;
                    break;
                case SelectedProfile.Last:
                    SelectedProfileIndex = this.Any() ? this.Count() - 1 : SelectedProfileIndex;
                    break;
            }
        }

        public IEnumerator<Profile> GetEnumerator() => ProfileList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal sealed class Profile
    {
        public static System.Version NoVersion { get; } = new System.Version("0.0");

        public static bool operator ==(Profile a, Profile b) => ReferenceEquals(a, null) ? ReferenceEquals(b, null) : a.Equals((object) b);
        public static bool operator !=(Profile a, Profile b) => !(a == b);

        public static async Task<IEnumerable<System.Version>> GetAvailableVersionsAsync(ProfileType profileType)
        {
            if (!GitHub.WebsiteIsUp)
                return new List<System.Version>();

            try
            {
                var t = await GitHub.GetAllGitHubReleasesAsync(profileType);
                return new List<System.Version>((await GitHub.GetAllGitHubReleasesAsync(profileType)).Select(release => release.Version)).OrderByDescending<System.Version, System.Version>(version => version); ;
            }
            catch (Exception) { return new List<System.Version>(); }
        }

        public static async Task Delete(Profile profile) => await profile.Folder.DeleteAsync();

        public static ProfileYaml ToYaml(Profile profile) => new ProfileYaml(profile.ProfileType, profile.Name, profile.Version, profile.LaunchArgs);
        public static Profile FromYaml(ProfileYaml yaml) => new Profile(yaml.ProfileType, yaml.Name, yaml.Version, yaml.LaunchArgs);


        public ProfileType ProfileType { get; }
        public string Name { get; }
        public System.Version Version { get; }
        public System.Version VersionExe => ExecutionFile != null ? new System.Version(FileVersionInfo.GetVersionInfo(ExecutionFile.Path).ProductVersion) : NoVersion;
        public string LaunchArgs { get; }

        public IFile ExecutionFile
        {
            get
            {
                if(AsyncExtensions.RunSync(async () => await Folder.CheckExistsAsync(ProfileType.Exe) == ExistenceCheckResult.FileExists))
                    return AsyncExtensions.RunSync(async () => await Folder.GetFileAsync(ProfileType.Exe));
                return null;
            }
        }
        public IFolder Folder => AsyncExtensions.RunSync(async () => await StorageInfo.GameProfilesFolder.CreateFolderAsync(Name, CreationCollisionOption.OpenIfExists));

        public bool IsDefault
        {
            get
            { 
                var latestVersion = AsyncExtensions.RunSync(async () => await GetAvailableVersionsAsync()).FirstOrDefault();
                return Name == "Latest" && latestVersion != null ? latestVersion == Version : Version == NoVersion;
            }
        }
        public bool IsSupportingGameJolt => ProfileType.IsSupportingGameJolt(Version);

        public Profile(ProfileType profileType, string name, System.Version version, string launchArgs = "")
        {
            ProfileType = profileType;
            Name = name;
            Version = version;
            LaunchArgs = string.IsNullOrEmpty(launchArgs) ? profileType.DefaultLaunchArgs : launchArgs;

            System.Version[] versions;
            if (Equals(Version, NoVersion) && (versions = AsyncExtensions.RunSync(async () => await GetAvailableVersionsAsync()).ToArray()).Length > 0)
                Version = versions.First();
        }

        public async Task<List<GitHubRelease>> GetAvailableReleasesAsync() => await GitHub.GetAllGitHubReleasesAsync(ProfileType);
        public async Task<IEnumerable<System.Version>> GetAvailableVersionsAsync() => await GetAvailableVersionsAsync(ProfileType);

        public ProfileYaml ToYaml() => ToYaml(this);
        public async Task Delete() => await Delete(this);

        private bool Equals(Profile other) => String.Equals(Name, other.Name);
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
