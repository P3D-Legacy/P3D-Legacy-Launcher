using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using P3D.Legacy.Launcher.Data;
using P3D.Legacy.Launcher.Storage.Folders;
using P3D.Legacy.Shared.Extensions;
using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

using YamlDotNet.Serialization;

namespace P3D.Legacy.Launcher.Storage.Files
{
    internal sealed class ProfilesFile : BaseYamlSettingsFile<ProfilesYaml>, IEnumerable<Profile>
    {
        protected override SerializerBuilder SerializerBuilder { get; } = ProfilesYaml.SerializerBuilder;
        protected override DeserializerBuilder DeserializerBuilder { get; } = ProfilesYaml.DeserializerBuilder;
        protected override ProfilesYaml Default { get; } = ProfilesYaml.Default;

        internal enum SelectedProfile { Current, First, Last }

        private Profile Latest => new Profile(ProfileType.Game, "Latest", AsyncExtensions.RunSync(async () => await Profile.GetAvailableVersionsAsync(ProfileType.Game)).FirstOrDefault() ?? Profile.NoVersion, string.Empty);
        private List<Profile> ProfileList { get; set; }

        public int SelectedProfileIndex { get; set; }
        public Profile CurrentProfile
        {
            get
            {
                if (ProfileList.Count <= SelectedProfileIndex) // Because of Latest
                    SelectedProfileIndex = ProfileList.Count - 1;
                return ProfileList.Any() ? ProfileList[SelectedProfileIndex] : null; // Because of Latest
            }
        }

        public ProfilesFile() : base(new MainFolder().CreateFile("Profiles.yml", CreationCollisionOption.OpenIfExists)) { }

        public async Task AddAsync(ProfileType profileType, string name, System.Version version, string launchArgs = "")
        {
            ProfileList.Add(new Profile(profileType, name, version, launchArgs));
            await SaveAsync();
        }
        public async Task AddAsync(Profile profile)
        {
            ProfileList.Add(profile);
            await SaveAsync();
        }

        public async Task DeleteAsync(int index)
        {
            if (ProfileList.Count > index && !ProfileList[index].IsDefault)
            {
                await ProfileList[index].DeleteAsync();
                ProfileList.RemoveAt(index);
            }
            await SaveAsync();
        }
        public async Task DeleteAsync(Profile profile)
        {
            if (ProfileList.Contains(profile) && !profile.IsDefault)
            {
                await profile.DeleteAsync();
                ProfileList.Remove(profile);
            }
            await SaveAsync();
        }

        public void Replace(Profile oldProfile, Profile newProfile)
        {
            for (var i = 0; i < ProfileList.Count; i++)
                if (ProfileList[i] == oldProfile)
                    ProfileList[i] = newProfile;

            if (oldProfile.Name != newProfile.Name)
            {
                var oldProfileFolder = oldProfile.Folder;
                var newProfileFolder = newProfile.Folder;
                if (oldProfileFolder.GetFiles().Any() || oldProfileFolder.GetFolders().Any())
                {
                    var folderReplace = GetEmptyProfileFolder(newProfile);

                    if (newProfileFolder.GetFiles().Any() || newProfileFolder.GetFolders().Any())
                    {
                        newProfileFolder.Move(folderReplace);
                        newProfileFolder = new ProfileFolder(newProfileFolder); // -- Recreate folder. Design flaw
                    }

                    oldProfileFolder.Move(newProfileFolder);
                }
            }
        }
        public async Task ReplaceAsync(Profile oldProfile, Profile newProfile)
        {
            for (var i = 0; i < ProfileList.Count; i++)
                if (ProfileList[i] == oldProfile)
                    ProfileList[i] = newProfile;

            if (oldProfile.Name != newProfile.Name)
            {
                var oldProfileFolder = oldProfile.Folder;
                var newProfileFolder = newProfile.Folder;
                if ((await oldProfileFolder.GetFilesAsync()).Any() || (await oldProfileFolder.GetFoldersAsync()).Any())
                {
                    var folderReplace = GetEmptyProfileFolder(newProfile);

                    if ((await newProfileFolder.GetFilesAsync()).Any() || (await newProfileFolder.GetFoldersAsync()).Any())
                    {
                        await newProfileFolder.MoveAsync(folderReplace);
                        newProfileFolder = new ProfileFolder(newProfileFolder); // -- Recreate folder. Design flaw
                    }

                    await oldProfileFolder.MoveAsync(newProfileFolder);
                }
            }
        }
        private static ProfileFolder GetEmptyProfileFolder(Profile profile)
        {
            var profileFolder = profile.Folder;
            while (true)
            {
                if (profileFolder.GetFiles().Any() || profileFolder.GetFolders().Any())
                {
                    profileFolder = new ProfileFolder($"{profileFolder.Name}_old", profile.ProfileType);
                    continue;
                }

                return profileFolder;
            }
        }

        public override void Save()
        {
            if (ProfileList.Count <= SelectedProfileIndex)
                SelectedProfileIndex = 0;

            base.Save();
        }

        public override async Task SaveAsync()
        {
            if (ProfileList.Count <= SelectedProfileIndex)
                SelectedProfileIndex = 0;

            await base.SaveAsync();
        }

        public void Reload(SelectedProfile selectedProfile = SelectedProfile.Current)
        {
            base.Reload();

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
        public async Task ReloadAsync(SelectedProfile selectedProfile = SelectedProfile.Current)
        {
            await base.ReloadAsync();

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

        protected override ProfilesYaml ToYaml()
        {
            ProfileList.Remove(Latest);
            return new ProfilesYaml(SelectedProfileIndex, ProfileList.Select(Profile.ToYaml).ToList());
        }

        protected override void FromYaml(ProfilesYaml profile)
        {
            SelectedProfileIndex = profile.SelectedProfileIndex;
            ProfileList = new List<Profile> {Latest};
            profile.ProfileList.RemoveAll(p => p.Name == "Latest"); // Removing old way of keeping latest profile
            ProfileList.AddRange(profile.ProfileList.Select(Profile.FromYaml).ToList());
        }


        public IEnumerator<Profile> GetEnumerator() => ProfileList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}