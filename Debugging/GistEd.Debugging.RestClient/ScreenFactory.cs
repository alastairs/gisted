using GistEd.Debugging.RestClient.Interfaces;
using Ninject;

namespace GistEd.Debugging.RestClient
{
    public class ScreenFactory : IScreenFactory
    {
        private readonly ILoginWindowView loginWindow;
        private readonly IGitHubRestClientWindow mainWindow;

        [Inject]
        public ScreenFactory(ILoginWindowView loginWindow, IGitHubRestClientWindow mainWindow)
        {
            this.loginWindow = loginWindow;
            this.mainWindow = mainWindow;
        }

        public ILoginWindowView GetLoginWindow()
        {
            return loginWindow;
        }

        public IGitHubRestClientWindow GetMainWindow()
        {
            return mainWindow;
        }
    }
}
