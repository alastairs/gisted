using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GistEd.GitHub.Tests
{
    [TestFixture]
    class GitHubGists_GetAllGistsForUser_Should
    {
        private const string User = "alastairs";
        private readonly GitHubGists gistApi = new GitHubGists(User);

        [Test]
        public void ReturnAListOfTheUsersGists_WhenTheUserIsValid()
        {
            var expectedGist = new Gist(1);

            IEnumerable<Gist> gists = gistApi.GetAllGistsForUser(new GitHubUser());

            Assert.AreEqual(expectedGist.Identity, gists.First().Identity);
        }
    }
}
