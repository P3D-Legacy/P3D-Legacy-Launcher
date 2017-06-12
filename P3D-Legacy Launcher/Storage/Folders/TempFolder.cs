using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Storage.Folders
{
    internal sealed class TempFolder : BaseFolder
    {
        public TempFolder() : base(new MainFolder().CreateFolder("Temp", CreationCollisionOption.OpenIfExists)) { }
    }
}