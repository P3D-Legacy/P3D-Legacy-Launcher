using System.Collections.Generic;

using Octokit;

namespace P3D.Legacy.Launcher
{
    public static class GitHubInfo
    {
        private const string GitHubClientHeader = "P3D.Legacy.Launcher";
        private const string GitHubOrgName = "P3D-Legacy";
        private const string GitHubRepoName = "P3D-Legacy";
        private static GitHubClient GitHubClient => new GitHubClient(new ProductHeaderValue(GitHubClientHeader));

        private static IEnumerable<Release> _getAllReleases;
        public static IEnumerable<Release> GetAllReleases => _getAllReleases ?? (_getAllReleases = GitHubClient.Repository.Release.GetAll(GitHubOrgName, GitHubRepoName).Result);

        public static void Update() => _getAllReleases = null;
    }
}