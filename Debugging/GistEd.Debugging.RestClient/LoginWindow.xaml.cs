using System.Net;
using System.Windows;
using System.Windows.Media;
using RestSharp;

namespace GistEd.Debugging.RestClient
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        private readonly IRestClient restClient;

        public GitHubToken Token { get; private set; }

        public LoginWindow(IRestClient restClient)
        {
            InitializeComponent();

            this.restClient = restClient;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            restClient.Authenticator = new HttpBasicAuthenticator(username, password);
            
            var request = new RestRequest
                              {
                                  Resource = string.Format("/user/{0}", username)
                              };

            var response = restClient.Execute(request);

            if (AuthenticationFailed(response.StatusCode))
            {
                Color semiTransparentRed = Color.FromArgb(0x50, 0xFF, 0x00, 0x00);

                txtUsername.Background = new SolidColorBrush(semiTransparentRed);
                txtPassword.Background = new SolidColorBrush(semiTransparentRed);

                return;
            }

            DialogResult = true;
            Token = new GitHubToken(username, password);
            Close();
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Token = null;
            DialogResult = false;
            Close();
        }
    }
}
