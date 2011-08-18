namespace GistEd.Debugging.RestClient.Interfaces
{
    public interface IScreenFactory
    {
        ILoginWindowView GetLoginWindow();
        IGitHubRestClientWindow GetMainWindow();
    }
}