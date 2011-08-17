using System;
using System.Linq;
using System.Windows;
using RestSharp;
using DataFormat = RestSharp.DataFormat;

namespace GistEd.Debugging.RestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IRestClient restClient;
        private GitHubToken token;
        private bool authenticating;
        private readonly LoginWindow loginWindow;

        public MainWindow()
        {
            InitializeComponent();
            
            restClient = new RestSharp.RestClient("https://api.github.com/");

            loginWindow = new LoginWindow(restClient)
                              {
                                  WindowStartupLocation = WindowStartupLocation.CenterOwner
                              };
        }

        private void BtnCloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnIssueRequestClick(object sender, RoutedEventArgs e)
        {
            var requestTokens = txtRequest.Text.Split(' ');
            
            Method requestMethod;
            Enum.TryParse(requestTokens.First(), true, out requestMethod);

            var requestResource = requestTokens.Skip(1).Take(1).Single();
            
            var request = new RestRequest
                              {
                                  Resource = requestResource,
                                  Method = requestMethod,
                                  RequestFormat = DataFormat.Json
                              };

            var response = restClient.Execute(request);
            
            txtResponse.Text = response.Content;
            txtResponseStatusCode.Text = string.Format("{0} {1}", (int) response.StatusCode, response.StatusDescription);
        }

        private void MainWindow_OnActivated(object sender, EventArgs e)
        {
            if (token != null || authenticating)
            {
                return;
            }

            authenticating = true;

            loginWindow.Owner = this;
            var result = loginWindow.ShowDialog() ?? false;
            
            authenticating = false;

            if (result)
            {
                token = loginWindow.Token;
                return;
            }
            
            Close();
        }
    }
}
