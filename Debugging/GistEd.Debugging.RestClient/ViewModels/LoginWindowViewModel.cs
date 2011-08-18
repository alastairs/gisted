using System;
using System.Net;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media;
using ReactiveUI;
using ReactiveUI.Xaml;
using RestSharp;

namespace GistEd.Debugging.RestClient.ViewModels
{
    public class LoginWindowViewModel : ReactiveObject
    {
        private static readonly Brush SemiTransparentRedBrush = new SolidColorBrush(Color.FromArgb(0x50, 0xFF, 0x00, 0x00));
        private static readonly Brush TransparentBrush = new SolidColorBrush(Colors.Transparent);

        private readonly ObservableAsPropertyHelper<Brush> _BackgroundBrush;
        private bool? _DialogResult;

        private string _Password;
        private GitHubToken _Token;
        private string _Username;

        public LoginWindowViewModel(IRestClient restClient)
        {
            IObservable<bool> canLogin = this.WhenAny(x => x.Username,
                                                      x => x.Password,
                                                      (u, p) => !string.IsNullOrWhiteSpace(u.Value) &&
                                                                !string.IsNullOrWhiteSpace(p.Value));

            LoginCommand = new ReactiveAsyncCommand(canLogin);
            IObservable<bool> loginResults = LoginCommand.RegisterAsyncFunction(x => Authenticate(restClient));
            
            IObservable<Brush> backgroundColourSelector = loginResults.Select(loginSucceeded => loginSucceeded ? TransparentBrush : SemiTransparentRedBrush);
            _BackgroundBrush = this.ObservableToProperty(backgroundColourSelector, x => x.BackgroundBrush);

            CancelCommand = new ReactiveCommand();
            CancelCommand.Subscribe(_ =>
                                        {
                                            Token = null;
                                            DialogResult = false;
                                        });
        }

        #region Data-Bound Properties

        public bool? DialogResult
        {
            get { return _DialogResult; }
            set { this.RaiseAndSetIfChanged(x => x.DialogResult, value); }
        }

        public GitHubToken Token
        {
            get { return _Token; }
            set { this.RaiseAndSetIfChanged(x => x.Token, value); }
        }

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

        public Brush BackgroundBrush
        {
            get
            {
                // The window will be displayed before any subscriptions have updated this helper.
                // Return null in this instance to keep the window open.
                if (_BackgroundBrush == null)
                {
                    return TransparentBrush;
                }

                return _BackgroundBrush.Value;
            }
        }

        #endregion

        #region Commands

        public IReactiveCommand CancelCommand { get; private set; }

        public ReactiveAsyncCommand LoginCommand { get; private set; }

        private bool Authenticate(IRestClient restClient)
        {
            restClient.Authenticator = new HttpBasicAuthenticator(Username, Password);
            var request = new RestRequest
                              {
                                  Resource = string.Format("/user/{0}", Username)
                              };
            RestResponse response = restClient.Execute(request);

            Thread.Sleep(TimeSpan.FromSeconds(5));

            if (!AuthenticationFailed(response.StatusCode))
            {
                Token = new GitHubToken(Username, Password);
                DialogResult = true;

                return true;
            }

            return false;
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