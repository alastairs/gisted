using System.Collections.Generic;

namespace GistEd.GitHub
{
    public interface IGitHubGists
    {
        IEnumerable<Gist> Get();
        Gist Get(int gistIdentity);
        Gist CreateGist(Gist gist);
    }
}