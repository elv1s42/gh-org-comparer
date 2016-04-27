using System;
using System.Collections.Generic;
using System.Linq;
using Octokit;

namespace OrgComparer.CustomClasses
{
    public class Org
    {
        public User Organization;
        public List<Repository> Repos;

        public List<Repository> GetNewRepos(DateTime lastUpdateMin)
        {
            return Repos.Where(r => r.UpdatedAt.DateTime >= lastUpdateMin).ToList();
        }

        public int GetNewReposNumber(DateTime lastUpdateMin)
        {
            return GetNewRepos(lastUpdateMin).Count;
        }

        public int GetNewReposStars(DateTime lastUpdateMin)
        {
            return GetNewRepos(lastUpdateMin).Sum(repo => repo.StargazersCount);
        }

        public int GetNewReposForks(DateTime lastUpdateMin)
        {
            return GetNewRepos(lastUpdateMin).Sum(repo => repo.ForksCount);
        }

        public StatInfo GetStats(DateTime lastUpdateMin)
        {
            return new StatInfo
            {
                Login = Organization.Login,
                LastRepoUpdateTime = lastUpdateMin,
                ReposCount = GetNewReposNumber(lastUpdateMin),
                ForksCount = GetNewReposForks(lastUpdateMin),
                StarsCount = GetNewReposStars(lastUpdateMin),
            };
        }
    }
}
