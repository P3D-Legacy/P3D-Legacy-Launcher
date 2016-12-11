using System.Linq;

using Octokit;

namespace P3D.Legacy.Launcher.Extensions
{
    public static class ReleaseExtensions
    {
        public static ReleaseAsset GetUpdateFeed(this Release release) => release.Assets?.SingleOrDefault(asset => asset.Name == "UpdateFeed.xml");
        public static ReleaseAsset GetRelease(this Release release) => release.Assets?.SingleOrDefault(asset => asset.Name == "Release.zip");
    }
}