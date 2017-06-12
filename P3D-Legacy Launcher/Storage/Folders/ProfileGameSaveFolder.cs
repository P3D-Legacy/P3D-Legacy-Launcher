using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Storage.Folders
{
    internal class ProfileGameSaveFolder : BaseFolder
    {
        public ProfileGameSaveFolder(ProfileFolder profileFolder) : base(profileFolder.CreateFolder("Save", CreationCollisionOption.OpenIfExists)) { }
    }
}