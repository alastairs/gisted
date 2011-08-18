using GistEd.Debugging.RestClient.Interfaces;
using GistEd.Debugging.RestClient.Views;
using Ninject;
using Ninject.Modules;
using RestSharp;

namespace GistEd.Debugging.RestClient
{
    public class GitHubRestClientAppModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGitHubRestClientWindow>().To<MainWindow>();
            Bind<ILoginWindowView>().To<LoginWindowView>();
            Bind<IRestClient>().ToMethod(_ => new RestSharp.RestClient("https://api.github.com/")).InSingletonScope();
            Bind<IKernel>().ToSelf();
        }
    }
}