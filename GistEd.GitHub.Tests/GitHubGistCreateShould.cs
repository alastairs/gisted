using NUnit.Framework;

namespace GistEd.GitHub.Tests
{
    [TestFixture]
    public class GitHubGistCreateShould
    {
        private readonly GitHubGists gistsApi = new GitHubGists("alastairs");
        
        [Test]
        public void ReturnTheCreatedGist_WithANewIdentity()
        {
            var gist = new Gist();

            gist = gistsApi.CreateGist(gist);

            Assert.AreEqual(1, gist.Identity);
        }

        [Test]
        public void NotReturnTheSameIdentityTwice()
        {
            var gist1 = new Gist();
            gist1 = gistsApi.CreateGist(gist1);

            var gist2 = new Gist();
            gist2 = gistsApi.CreateGist(gist2);

            Assert.AreNotEqual(gist1.Identity, gist2.Identity);
        }
    }
}
