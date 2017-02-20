using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Octokit;

using P3D.Legacy.Launcher.Data;

namespace P3D.Legacy.Launcher.Services
{
    internal static class GitHub
    {
        private const string Host = "github.com";

        private const string ClientHeader = "P3D.Legacy.Launcher";

        private const string OrgName = "P3D-Legacy";
        private const string GameRepoName = "P3D-Legacy";
        private const string LauncherRepoName = "P3D-Legacy-Launcher";

        private static GitHubClient Client => new GitHubClient(new ProductHeaderValue(ClientHeader));

        private static WebsiteChecker WebsiteChecker { get; } = new WebsiteChecker(Host);
        public static bool WebsiteIsUp => WebsiteChecker.Check();

        private static List<Release> _getAllReleases;
        private static async Task<List<Release>> GetAllReleases()
        {
            if (_getAllReleases != null) return _getAllReleases;
            else
            {
                if(!WebsiteIsUp) return new List<Release>();

                try { return _getAllReleases = new List<Release>((await Client.Repository.Release.GetAll(OrgName, GameRepoName)).OrderByDescending(release => new Version(release.TagName))); }
                catch (Exception) { return new List<Release>(); }
            }
        }
        public static async Task<List<GitHubRelease>> GetAllGitHubReleases()
        {
            var releases = await GetAllReleases();
            return new List<GitHubRelease>(releases.Any() ? releases.Select(release => new GitHubRelease(release)) : new List<GitHubRelease>());
        }

        private static List<Release> _getAllLauncherReleases;
        private static async Task<List<Release>> GetAllLauncherReleases()
        {
            if (_getAllLauncherReleases != null) return _getAllLauncherReleases;
            else
            {
                if (!WebsiteIsUp) return new List<Release>();

                try { return _getAllLauncherReleases = new List<Release>(await Client.Repository.Release.GetAll(OrgName, LauncherRepoName)); }
                catch (Exception) { return new List<Release>(); }
            }
        }
        public static async Task<List<GitHubRelease>> GetAllLauncherGitHubReleases()
        {
            var releases = await GetAllLauncherReleases();
            return new List<GitHubRelease>(releases.Any() ? releases.Select(release => new GitHubRelease(release)) : new List<GitHubRelease>());
        }

        private static List<Release> _getBetaWhitelistUsers;
        private static async Task<List<Release>> GetBetaWhitelistUsers()
        {
            if (_getBetaWhitelistUsers != null) return _getBetaWhitelistUsers;
            else
            {
                if (!WebsiteIsUp) return new List<Release>();

                try { return _getBetaWhitelistUsers = new List<Release>(await Client.Repository.Release.GetAll(OrgName, LauncherRepoName)); }
                catch (Exception) { return new List<Release>(); }
            }
        }


        public static void Update()
        {
            _getAllReleases = null;
            _getAllLauncherReleases = null;
        }
    }
}