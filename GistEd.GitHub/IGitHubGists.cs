using System.Collections.Generic;

namespace GistEd.GitHub
{
    /// <summary>
    /// The main API for interacting with GitHub's Gists.
    /// </summary>
    public interface IGitHubGists
    {
        /// <summary>
        /// Returns all the Gists owned by the specified user.
        /// </summary>
        /// <param name="user">The user for which the Gists should be returned</param>
        /// <returns></returns>
        IEnumerable<Gist> GetAllGistsForUser(GitHubUser user);

        /// <summary>
        /// Gets the Gist identified by the specified identity.  Returns <c>null</c> if
        /// the identity references a private Gist owned by another user.  
        /// </summary>
        /// <param name="gistIdentity">The gist identity.</param>
        /// <returns>The Gist identified by the specified identity, or <c>null</c>.</returns>
        Gist Get(int gistIdentity);

        /// <summary>
        /// Creates a new Gist owned by the specified user.
        /// </summary>
        /// <param name="gist">The gist.</param>
        /// <param name="user">The user creating the Gist.</param>
        /// <returns>The created gist, updated with its Identity.</returns>
        Gist CreateGist(Gist gist, GitHubUser user);
    }
}