using System;

using Octokit;

using P3D.Legacy.Launcher.Extensions;

namespace P3D.Legacy.Launcher.Data
{
    public class OnlineGameRelease
    {
        private Release Release { get; }
        public ReleaseAsset ReleaseAsset => Release.GetRelease();
        public ReleaseAsset UpdateFeedAsset => Release.GetUpdateFeed();

        public Version Version => new Version(Release.TagName ?? "0.0");
        public DateTime ReleaseDate => Release.CreatedAt.DateTime;

        public OnlineGameRelease(Release release) { Release = release; }
    }
}