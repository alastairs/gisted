using System.Net;
using System.Windows;
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

            if (!AuthenticationFailed(response.StatusCode))
            {
                DialogResult = true;
                Token = new GitHubToken(username, password);
                Close();

                return;
            }

            MessageBox.Show("Your username or password was incorrect. Please try again.");
        }

        private static bool AuthenticationFailed(HttpStatusCode statusCode)
        {
            return statusCode == HttpStatusCode.Forbidden ||
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
