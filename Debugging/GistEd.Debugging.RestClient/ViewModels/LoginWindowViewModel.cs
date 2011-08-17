using System;
using System.Net;
using System.Windows.Media;
using ReactiveUI;
using ReactiveUI.Xaml;
using RestSharp;

namespace GistEd.Debugging.RestClient.ViewModels
{
    public class LoginWindowViewModel : ReactiveObject
    {
        private static readonly Color SemiTransparentRed = Color.FromArgb(0x50, 0xFF, 0x00, 0x00);

        private string _Password;
        private string _Username;

        public LoginWindowViewModel(IRestClient restClient)
        {
            IObservable<bool> canLogin = this.WhenAny(x => x.Username,
                                                      x => x.Password,
                                                      (u, p) => !string.IsNullOrWhiteSpace(u.Value) && !string.IsNullOrWhiteSpace(p.Value));

            LoginCommand = new ReactiveCommand(canLogin);
            LoginCommand.Subscribe(_ => Authenticate(restClient));

            CancelCommand = new ReactiveCommand();
            CancelCommand.Subscribe(_ =>
                                        {
                                            Token = null;
                                        });
        }

        public GitHubToken Token { get; private set; }

        public string Password
        {
            get { return _Password; }
            set { this.RaiseAndSetIfChanged(x => x.Password, value); }
        }

        public string Username
        {
            get { return _Username; }
            set { this.RaiseAndSetIfChanged(x => x.Username, value); }
        }

        public Color BackgroundColor { get; set; }

        #region Commands

        public IReactiveCommand CancelCommand { get; private set; }

        public IReactiveCommand LoginCommand { get; private set; }

        private void Authenticate(IRestClient restClient)
        {
            restClient.Authenticator = new HttpBasicAuthenticator(Username, Password);

            var request = new RestRequest
                              {
                                  Resource = string.Format("/user/{0}", Username)
                              };

            RestResponse response = restClient.Execute(request);

            if (AuthenticationFailed(response.StatusCode))
            {
                BackgroundColor = SemiTransparentRed;
                Token = null;
                return;
            }

            BackgroundColor = Colors.Transparent;
            Token = new GitHubToken(Username, Password);
        }

        private static bool AuthenticationFailed(HttpStatusCode statusCode)
        {
            return statusCode == HttpStatusCode.Unauthorized ||
                   statusCode == HttpStatusCode.Forbidden ||
                   statusCode == HttpStatusCode.BadGateway ||
                   statusCode == HttpStatusCode.BadRequest ||
                   statusCode == HttpStatusCode.InternalServerError ||
                   statusCode == HttpStatusCode.RequestTimeout;
        }

        #endregion
    }
}