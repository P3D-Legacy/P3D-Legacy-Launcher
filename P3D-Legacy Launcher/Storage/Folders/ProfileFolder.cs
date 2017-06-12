using P3D.Legacy.Launcher.Data;
using P3D.Legacy.Launcher.Storage.Files;

using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Storage.Folders
{
    internal sealed class ProfileFolder : BaseFolder
    {
        private string ProfileName { get; }
        private ProfileType ProfileType { get; }
        public ProfileExeFile ExecutionFile => CheckExists(ProfileType.Exe) == ExistenceCheckResult.FileExists ? new ProfileExeFile(ProfileType.Exe, this) : null;
        public ModificationsFolder ModFolder => new ModificationsFolder(CreateFolder(ProfileType.ModFolder, CreationCollisionOption.OpenIfExists));
        public ProfileGameGameModeFolder GameModeFolder => new ProfileGameGameModeFolder(CreateFolder(ProfileType.ModFolder, CreationCollisionOption.OpenIfExists));

        public ProfileFolder(string profileName, ProfileType profileType) : base(new ProfilesFolder().CreateFolder(profileName, CreationCollisionOption.OpenIfExists)) { ProfileName = profileName; ProfileType = profileType; }
        public ProfileFolder(ProfileFolder profileFolder) : base(new ProfilesFolder().CreateFolder(profileFolder.Name, CreationCollisionOption.OpenIfExists)) { ProfileName = profileFolder.ProfileName; ProfileType = profileFolder.ProfileType; }
    }
}
