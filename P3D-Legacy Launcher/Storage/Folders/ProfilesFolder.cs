using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Storage.Folders
{
    internal sealed class ProfilesFolder : BaseFolder
    {
        public ProfilesFolder() : base(new MainFolder().CreateFolder("Profiles", CreationCollisionOption.OpenIfExists)) { }
    }
}