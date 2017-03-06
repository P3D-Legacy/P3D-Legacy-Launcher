using System.Threading.Tasks;

using P3D.Legacy.Shared.Storage.Folders;

using PCLExt.FileStorage;

namespace P3D.Legacy.Launcher.Services
{
    internal static class StorageInfo
    {
        public static IFolder MainFolder => FileSystem.Current.BaseStorage;

        private const string UpdateFoldername = "Update";
        public static IFolder UpdateFolder => MainFolder.CreateFolderAsync(UpdateFoldername, CreationCollisionOption.OpenIfExists).Result;

        private const string LanguagesFoldername = "Languages";
        public static ILocalizationFolder LocalizationFolder => new LocalizationFolder(MainFolder.CreateFolderAsync(LanguagesFoldername, CreationCollisionOption.OpenIfExists).Result);

        private const string TempFoldername = "Temp";
        public static IFolder TempFolder => MainFolder.CreateFolderAsync(TempFoldername, CreationCollisionOption.OpenIfExists).Result;
        public static async Task<IFile> GetTempFile(string fileName) => await TempFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);

        public const string ExeFilename = "Pokemon3D.exe";

        public const string UpdaterExeFilename = "P3D-Legacy Launcher Updater.exe";

        private const string SettingsFilename = "Settings.yml";
        public static IFile SettingsFile => MainFolder.CreateFileAsync(SettingsFilename, CreationCollisionOption.OpenIfExists).Result;

        private const string ProfilesFilename = "Profiles.yml";
        public static IFile ProfilesFile => MainFolder.CreateFileAsync(ProfilesFilename, CreationCollisionOption.OpenIfExists).Result;

        private const string GameProfilesFoldername = "Profiles";
        public static IFolder GameProfilesFolder => MainFolder.CreateFolderAsync(GameProfilesFoldername, CreationCollisionOption.OpenIfExists).Result;
    }
}