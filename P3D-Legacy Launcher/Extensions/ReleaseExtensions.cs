using System;
using System.Linq;

using Octokit;

namespace P3D.Legacy.Launcher.Extensions
{
    internal static class ReleaseExtensions
    {
        private const string ReleaseFilename = "Release.zip";
        private const string UpdateInfoFilename = "UpdateInfo.yml";

        public static bool IsValid(this Release release) => release.Assets.Any(releaseAsset => string.Equals(releaseAsset.Name, ReleaseFilename, StringComparison.OrdinalIgnoreCase));

        public static ReleaseAsset GetUpdateInfo(this Release release) => release.Assets?.SingleOrDefault(asset => string.Equals(asset.Name, UpdateInfoFilename, StringComparison.OrdinalIgnoreCase));
        public static ReleaseAsset GetRelease(this Release release) => release.Assets.Count > 1 ? release.Assets?.SingleOrDefault(asset => string.Equals(asset.Name, ReleaseFilename, StringComparison.OrdinalIgnoreCase)) : release.Assets.First();
    }
}