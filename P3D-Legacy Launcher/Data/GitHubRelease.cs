using System;

using Octokit;

using P3D.Legacy.Launcher.Extensions;

namespace P3D.Legacy.Launcher.Data
{
    internal class GitHubRelease
    {
        private Release Release { get; }
        public ReleaseAsset ReleaseAsset => Release.GetRelease();
        public ReleaseAsset UpdateInfoAsset => Release.GetUpdateInfo();

        public Version Version => new Version(Release.TagName ?? "0.0");
        public DateTime ReleaseDate => Release.CreatedAt.DateTime;

        public GitHubRelease(Release release) { Release = release; }
    }
}