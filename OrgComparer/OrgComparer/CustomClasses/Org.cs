using System.Collections.Generic;
using Octokit;

namespace OrgComparer.CustomClasses
{
    public class Org
    {
        public User Organization;
        public List<Repository> Repos;
    }
}
