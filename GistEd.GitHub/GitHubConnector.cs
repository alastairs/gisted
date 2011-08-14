using System;
using System.IO;
using System.Net;
using RestSharp;

namespace GistEd.GitHub
{
    internal class GitHubConnector
    {
        private static readonly Uri GitHubAddress = new Uri("https://api.github.com");
        
        private readonly RestClient restClient;

        public GitHubConnector(RestClient restClient)
        {
            this.restClient = new RestClient
                                  {
                                      BaseUrl = "https://api.github.com/",
                                      UserAgent = "GistEd"
                                  };
        }

        public GitHubToken Authenticate(string username, string password)
        {
            restClient.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest
                              {
                                  Resource = "/users/alastairs/gists"
                              };

            var response = restClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return new GitHubToken();
            }

            throw new GitHubAuthenticationException(response.Content);
        }
    }
}
