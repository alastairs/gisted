using NUnit.Framework;

namespace GistEd.GitHub.Tests
{
    [TestFixture]
    public class GitHubConnectorAuthenticateShould
    {
        [Test]
        public void ReturnAGitHubToken_WhenAuthenticationIsSuccessful()
        {
            var connector = new GitHubConnector();
            GitHubToken token = connector.Authenticate("alastairs");

            Assert.IsNotNull(token);
        }
    }
}
