using System.Collections.Generic;
using System.Linq;

using P3D.Legacy.Launcher.Storage.Files;

using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Storage.Folders
{
    public class ProfileGameGameModeFolder : BaseFolder
    {
        public ProfileGameGameModeFolder(IFolder folder) : base(folder) { }

        public IList<ProfileGameGameModeFile> GetGameModes()
        {
            return GetFolders()
                .Where(folder => folder.CheckExists(ProfileGameGameModeFile.FileName) == ExistenceCheckResult.FileExists)
                .Select(folder => new ProfileGameGameModeFile(folder.GetFile(ProfileGameGameModeFile.FileName)))
                .ToList();
        }
    }
}
