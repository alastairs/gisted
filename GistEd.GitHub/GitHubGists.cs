using System.Collections.Generic;

namespace GistEd.GitHub
{
    public class GitHubGists
    {
        private readonly string user;

        public GitHubGists(string user)
        {
            this.user = user;
        }

        public IEnumerable<Gist> Get()
        {
            yield return new Gist(1);
        }

        public Gist Get(int gistIdentity)
        {
            return new Gist(gistIdentity);
        }
    }
}
