namespace GistEd.GitHub
{
    public interface IGitHubClient
    {
        GitHubUser Authenticate(string username, string password);
    }
}