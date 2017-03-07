using System;
using System.Linq;

using Octokit;

namespace P3D.Legacy.Launcher.Extensions
{
    internal static class ReleaseExtensions
    {
        public static ReleaseAsset GetUpdateInfo(this Release release) => release.Assets?.SingleOrDefault(asset => string.Equals(asset.Name, "UpdateInfo.yml", StringComparison.OrdinalIgnoreCase));
        public static ReleaseAsset GetRelease(this Release release) => release.Assets.Count > 1 ? release.Assets?.SingleOrDefault(asset => string.Equals(asset.Name, "Release.zip", StringComparison.OrdinalIgnoreCase)) : release.Assets.First();
        public static bool HasRelease(this Release release) => release.Assets.Any(releaseAsset => string.Equals(releaseAsset.Name, "Release.zip", StringComparison.OrdinalIgnoreCase));
    }
}