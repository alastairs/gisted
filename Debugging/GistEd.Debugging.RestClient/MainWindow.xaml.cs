using System;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;
using Ninject;
using RestSharp;
using DataFormat = RestSharp.DataFormat;

namespace GistEd.Debugging.RestClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IGitHubRestClientWindow
    {
        private readonly IRestClient restClient;
        private GitHubToken token;
        private bool authenticating;
        private readonly ILoginWindowView loginWindowView;

        [Inject]
        public MainWindow(IRestClient restClient, ILoginWindowView loginWindowView)
        {
            InitializeComponent();

            this.restClient = restClient;
            this.loginWindowView = loginWindowView;
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
            
            txtResponse.Text = GetPrettyPrintedJson(response.Content);
            txtResponseStatusCode.Text = string.Format("{0} {1}", (int) response.StatusCode, response.StatusDescription);
        }

        private void MainWindow_OnActivated(object sender, EventArgs e)
        {
            if (token != null || authenticating)
            {
                return;
            }

            authenticating = true;

            loginWindowView.Owner = this;
            var result = loginWindowView.ShowDialog() ?? false;
            
            authenticating = false;

            if (result)
            {
                token = loginWindowView.Token;
                return;
            }
            
            Close();
        }

        public static string GetPrettyPrintedJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
    }
}
