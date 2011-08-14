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
            var connector = new GitHubConnector(null);

            Assert.Throws<GitHubAuthenticationException>(() => connector.Authenticate("alastairs", "password"));
        }
    }
}
