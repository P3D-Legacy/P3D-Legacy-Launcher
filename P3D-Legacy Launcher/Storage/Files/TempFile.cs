using P3D.Legacy.Launcher.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Storage.Files
{
    internal sealed class TempFile : BaseFile
    {
        public TempFile(string name) : base(new TempFolder().CreateFile(name, CreationCollisionOption.OpenIfExists)) { }
    }
}
