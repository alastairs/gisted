namespace GistEd.GitHub
{
    public class GitHubToken
    {
        public int UserIdentity { get; private set; }

        public GitHubToken(int userIdentity)
        {
            UserIdentity = userIdentity;
        }
    }
}