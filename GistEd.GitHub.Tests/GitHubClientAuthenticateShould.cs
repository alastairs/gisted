using System.Net;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace GistEd.GitHub.Tests
{
    [TestFixture]
    public class GitHubClientAuthenticateShould
    {
        [Test]
        public void ThrowAGitHubAuthenticationException_WhenThePasswordIsIncorrect()
        {
            const string username = "alastairs";
            const string password = "password";
            
            var restClient = new Mock<IRestClient>();
            restClient.SetupAllProperties();

            var restResponse = new RestResponse<GitHubUser> {StatusCode = HttpStatusCode.Forbidden};
            restClient.Setup(c => c.Execute<GitHubUser>(It.IsAny<RestRequest>())).Returns(restResponse);

            var gitHubClient = new GitHubClient(restClient.Object);

            Assert.Throws<GitHubAuthenticationException>(() => gitHubClient.Authenticate(username, password));
        }

        [Test]
        public void ThrowAGitHubAuthenticationException_WhenTheUsernameIsIncorrect()
        {
            const string username = "username";
            const string password = "password";
            
            var restClient = new Mock<IRestClient>();
            restClient.SetupAllProperties();

            var restResponse = new RestResponse<GitHubUser> {StatusCode = HttpStatusCode.Forbidden};
            restClient.Setup(c => c.Execute<GitHubUser>(It.IsAny<RestRequest>())).Returns(restResponse);

            var gitHubClient = new GitHubClient(restClient.Object);

            Assert.Throws<GitHubAuthenticationException>(() => gitHubClient.Authenticate(username, password));
        }

        [Test]
        public void ReturnAGitHubTokenObject_WhenTheCredentialsAreValid()
        {
            const string username = "username";
            const string password = "password";

            var expectedUser = new GitHubUser
                           {
                               Identity = 1,
                               Username = username
                           };

            var restClient = new Mock<IRestClient>();
            restClient.SetupAllProperties();

            var restResponse = new RestResponse<GitHubUser> {StatusCode = HttpStatusCode.OK, Data = expectedUser};
            restClient.Setup(c => c.Execute<GitHubUser>(It.IsAny<RestRequest>())).Returns(restResponse);

            var gitHubClient = new GitHubClient(restClient.Object);
            var actualUser = gitHubClient.Authenticate(username, password);

            Assert.AreEqual(expectedUser.Identity, actualUser.Identity);
        }
    }
}
