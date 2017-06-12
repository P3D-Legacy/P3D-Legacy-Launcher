using P3D.Legacy.Shared.Data;

using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Storage.Files
{
    public class ProfileGameGameModeFile : BaseFile
    {
        public const string FileName = "GameMode.dat";

        public ModificationInfo ModificationInfo { get; }

        public ProfileGameGameModeFile(IFile file) : base(file)
        {
            var data = this.ReadAllLines();
            var name = data[0].Split('|')[1];

            ModificationInfo = new ModificationInfo() { Name = name };
        }
    }
}
