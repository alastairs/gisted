using Moq;
using NUnit.Framework;

namespace GistEd.GitHub.Tests
{
    [TestFixture]
    public class GitHubConnectorAuthenticateShould
    {
        [Test]
        public void ThrowAGitHubAuthenticationException_WhenThePasswordIsIncorrect()
        {
            const string username = "alastairs";
            const string password = "password";
            
            var client = new Mock<IGitHubClient>();
            client.Setup(c => c.Authenticate(username, password)).Throws<GitHubAuthenticationException>();
            
            var connector = new GitHubConnector(client.Object);
            Assert.Throws<GitHubAuthenticationException>(() => connector.Authenticate(username, password));
        }

        [Test]
        public void ThrowAGitHubAuthenticationException_WhenTheUsernameIsIncorrect()
        {
            const string username = "username";
            const string password = "password";
            
            var client = new Mock<IGitHubClient>();
            client.Setup(c => c.Authenticate(username, password)).Throws<GitHubAuthenticationException>();
            
            var connector = new GitHubConnector(client.Object);
            Assert.Throws<GitHubAuthenticationException>(() => connector.Authenticate(username, password));
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

            var client = new Mock<IGitHubClient>();
            client.Setup(c => c.Authenticate(username, password)).Returns(expectedUser);

            var connector = new GitHubConnector(client.Object);

            var token = connector.Authenticate(username, password);

            Assert.AreEqual(expectedUser.Identity, token.UserIdentity);
        }
    }
}
