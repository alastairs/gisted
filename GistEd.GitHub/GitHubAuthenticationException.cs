using System;

namespace GistEd.GitHub
{
    public class GitHubAuthenticationException : Exception
    {
        public GitHubAuthenticationException(string message) : base(message)
        {
        }
    }
}
