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
            return new Gist(1);
        }
    }
}
