using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Storage.Folders
{
    internal sealed class UpdateFolder : BaseFolder
    {
        public UpdateFolder() : base(new MainFolder().CreateFolder("Update", CreationCollisionOption.OpenIfExists)) { }
    }
}