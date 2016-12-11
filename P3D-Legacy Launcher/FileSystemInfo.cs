using System;
using System.IO;

namespace P3D.Legacy.Launcher
{
    public static class FileSystemInfo
    {
        public static string MainFolderPath { get; } = AppDomain.CurrentDomain.BaseDirectory;

        private const string UpdateFoldername = "Update";
        public static string UpdateFolderPath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UpdateFoldername);

        private const string TempFoldername = "Temp";
        public static string TempFolderPath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TempFoldername);
        public static string TempFilePath(string fileName) => Path.Combine(TempFolderPath, fileName);

        public const string ExeFilename = "Pokemon3D.exe";

        public const string UpdaterExeFilename = "P3D-Legacy Launcher Updater.exe";

        private const string SettingsFilename = "Settings.yml";
        public static string SettingsFilePath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SettingsFilename);

        private const string ProfilesFilename = "Profiles.yml";
        public static string ProfilesFilePath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ProfilesFilename);

        private const string GameReleasesFoldername = "Releases";
        public static string GameReleasesFolderPath { get; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GameReleasesFoldername);
    }
}