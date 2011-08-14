using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GistEd.GitHub.Tests
{
    [TestFixture]
    public class GitHubGistGetShould
    {
        private const string User = "alastairs";
        private readonly GitHubGists gistApi = new GitHubGists(User);

        [Test]
        public void ReturnAListOfTheUsersGists_WhenTheUsersIdIsValid()
        {
            var expectedGist = new Gist(1);

            IEnumerable<Gist> gists = gistApi.Get();

            Assert.AreEqual(expectedGist.Identity, gists.First().Identity);
        }

        [Test]
        public void ReturnATheSpecifiedGist_WhenTheGistIdentityIsValid_AndTheGistBelongsToTheCurrentUser()
        {
            const int gistIdentity = 1;
            var expectedGist = new Gist(gistIdentity);

            Gist gist = gistApi.Get(gistIdentity);

            Assert.AreEqual(expectedGist.Identity, gist.Identity);
        }
    }
}