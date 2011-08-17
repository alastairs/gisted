using System;
using System.Collections.Generic;

namespace GistEd.GitHub
{
    public class GitHubGists : IGitHubGists
    {
        private int newIdentity = 1;

        public GitHubGists(string user)
        {
        }

        public IEnumerable<Gist> GetAllGistsForUser(GitHubUser user)
        {
            yield return new Gist(1);
        }

        public Gist Get(int gistIdentity)
        {
            if (gistIdentity < 0)
            {
                throw new ArgumentException("Parameter must be positive", "gistIdentity");
            }

            if (gistIdentity == 0 || gistIdentity == 3)
            {
                throw new GistNotFoundException(gistIdentity);
            }

            return new Gist(gistIdentity);
        }

        public Gist CreateGist(Gist gist, GitHubUser user)
        {
            return new Gist(newIdentity++);
        }
    }
}
