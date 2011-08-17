using GistEd.Debugging.RestClient.ViewModels;

namespace GistEd.Debugging.RestClient
{
    public interface ILoginWindowView : IWindow
    {
        GitHubToken Token { get; }
        LoginWindowViewModel ViewModel { get; }
    }
}