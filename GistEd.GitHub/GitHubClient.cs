using System.Net;
using Ninject;
using RestSharp;

namespace GistEd.GitHub
{
    internal class GitHubClient : IGitHubClient
    {
        private const string GitHubApiAddress = "https://api.github.com/";
        private const string GistEdUserAgentString = "GistEd";

        private readonly IRestClient restClient;

        [Inject]
        public GitHubClient(IRestClient restClient)
        {
            this.restClient = restClient;
            this.restClient.BaseUrl = GitHubApiAddress;
            this.restClient.UserAgent = GistEdUserAgentString;
        }

        public GitHubUser Authenticate(string username, string password)
        {
            restClient.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest
                              {
                                  Resource = string.Format("/user/{0}", username),
                                  RootElement = "user"
                              };

            var response = restClient.Execute<GitHubUser>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Data;
            }

            throw new GitHubAuthenticationException(response.StatusDescription);
        }
    }
}
