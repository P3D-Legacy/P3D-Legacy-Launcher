using System.Linq;

using Octokit;

namespace P3D.Legacy.Launcher.Extensions
{
    public static class ReleaseExtensions
    {
        public static ReleaseAsset GetUpdateInfo(this Release release) => release.Assets?.SingleOrDefault(asset => asset.Name == "UpdateInfo.yml");
        public static ReleaseAsset GetRelease(this Release release) => release.Assets?.SingleOrDefault(asset => asset.Name == "Release.zip");
    }
}