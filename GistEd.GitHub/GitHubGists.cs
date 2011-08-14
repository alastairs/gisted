using System;
using System.Collections.Generic;

namespace GistEd.GitHub
{
    public class GitHubGists
    {
        public GitHubGists(string user)
        {
        }

        public IEnumerable<Gist> Get()
        {
            yield return new Gist(1);
        }

        public Gist Get(int gistIdentity)
        {
            if (gistIdentity < 0)
            {
                throw new ArgumentException("Parameter must be positive", "gistIdentity");
            }

            if (gistIdentity == 0)
            {
                throw new GistNotFoundException(gistIdentity);
            }

            return new Gist(gistIdentity);
        }
    }
}
