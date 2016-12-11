using System;
using System.Collections.Generic;
using System.Linq;

using Octokit;

namespace P3D.Legacy.Launcher
{
    public static class GitHubInfo
    {
        private const string GitHubClientHeader = "P3D.Legacy.Launcher";
        private const string GitHubOrgName = "P3D-Legacy";
        private const string GitHubRepoName = "P3D-Legacy";
        private static GitHubClient GitHubClient => new GitHubClient(new ProductHeaderValue(GitHubClientHeader));

        public static IEnumerable<Release> GetAllReleases
        {
            get
            {
                try { return GitHubClient.Repository.Release.GetAll(GitHubOrgName, GitHubRepoName).Result.ToList(); }
                catch (Exception) { return new List<Release>(); }
            }
        }
    }
}