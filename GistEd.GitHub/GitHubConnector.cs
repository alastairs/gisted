namespace GistEd.GitHub
{
    internal class GitHubConnector
    {
        private readonly IGitHubClient client;

        public GitHubConnector(IGitHubClient client)
        {
            this.client = client;
        }

        public GitHubToken Authenticate(string username, string password)
        {
            GitHubUser user = client.Authenticate(username, password);

            return new GitHubToken(user.Identity);
        }
    }
}
