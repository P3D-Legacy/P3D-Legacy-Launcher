using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Octokit;
using Octokit.Internal;

using P3D.Legacy.Launcher.Data;
using P3D.Legacy.Launcher.Extensions;
using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Extensions;

using static P3D.Legacy.Shared.Data.EncodedString;

namespace P3D.Legacy.Launcher.Services
{
    internal static class GitHub
    {
        private static readonly EncodedString Host = "github.com";

        private static readonly EncodedString ClientHeader = "P3D.Legacy.Launcher";
        private static readonly EncodedString ClientToken = "";

        private static readonly EncodedString OrgName = "P3D-Legacy";
        private static readonly EncodedString GameRepoName = "P3D-Legacy";
        private static readonly EncodedString LauncherRepoName = "P3D-Legacy-Launcher";

        private static readonly EncodedString OrgServer1Name = "PokeD";
        private static readonly EncodedString Server1RepoName = "PokeD.Server.Desktop";

        private static readonly EncodedString OrgServer2Name = "jianmingyong";
        private static readonly EncodedString Server2RepoName = "Pokemon-3D-Server-Client";

        private static GitHubClient Client => AsyncExtensions.RunSync(async () => await AnonymousHitRateLimit()) ? TokenClient : AnonymousClient;
        private static GitHubClient AnonymousClient { get; } = new GitHubClient(new Connection(new ProductHeaderValue(ClientHeader)));
        private static GitHubClient TokenClient { get; } = new GitHubClient(new Connection(new ProductHeaderValue(ClientHeader), new InMemoryCredentialStore(new Credentials(new EncodedString(FromEncodedData(ClientToken))))));

        private static WebsiteChecker WebsiteChecker { get; } = new WebsiteChecker(Host);
        public static bool WebsiteIsUp => WebsiteChecker.Check();

        private static Dictionary<ProfileType, List<GitHubRelease>> _getAllReleases = new Dictionary<ProfileType, List<GitHubRelease>>();
        public static async Task<List<GitHubRelease>> GetAllReleasesAsync(ProfileType profileType)
        {
            if (_getAllReleases.ContainsKey(profileType)) return _getAllReleases[profileType];
            else
            {
                if(!WebsiteIsUp) return new List<GitHubRelease>();

                try
                {
                    var options = new ApiOptions() { StartPage = 1, PageCount = 1, PageSize = 30 };
                    if (profileType == ProfileType.Game)
                        _getAllReleases.Add(profileType, FilterReleases(await Client.Repository.Release.GetAll(OrgName, GameRepoName, options)));
                    else if (profileType == ProfileType.Server1)
                        _getAllReleases.Add(profileType, FilterReleases(await Client.Repository.Release.GetAll(OrgServer1Name, Server1RepoName, options)));
                    else if (profileType == ProfileType.Server2)
                        _getAllReleases.Add(profileType, FilterReleases(await Client.Repository.Release.GetAll(OrgServer2Name, Server2RepoName, options)));
                    return _getAllReleases[profileType];
                }
                catch (Exception) { return new List<GitHubRelease>(); }
            }
        }
        private static List<GitHubRelease> FilterReleases(IEnumerable<Release> releases) => releases.Where(release => release.IsValid()).Select(release => new GitHubRelease(release)).OrderByDescending(githubRelease => githubRelease.Version).ToList();

        private static List<GitHubRelease> _getAllLauncherReleases;
        public static async Task<List<GitHubRelease>> GetAllLauncherReleasesAsync()
        {
            if (_getAllLauncherReleases != null) return _getAllLauncherReleases;
            else
            {
                if (!WebsiteIsUp) return new List<GitHubRelease>();

                try { return _getAllLauncherReleases = (await Client.Repository.Release.GetAll(OrgName, LauncherRepoName)).Select(release => new GitHubRelease(release)).ToList(); }
                catch (Exception) { return new List<GitHubRelease>(); }
            }
        }

        private static async Task<bool> AnonymousHitRateLimit() => (await AnonymousClient.Miscellaneous.GetRateLimits()).Resources.Core.Remaining <= 0;

        public static void Update()
        {
            _getAllReleases = new Dictionary<ProfileType, List<GitHubRelease>>();
            _getAllLauncherReleases = null;
        }
    }
}