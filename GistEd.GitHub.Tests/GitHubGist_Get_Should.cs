using System;
using NUnit.Framework;

namespace GistEd.GitHub.Tests
{
    [TestFixture]
    public class GitHubGist_Get_Should
    {
        private const string User = "alastairs";
        private readonly GitHubGists gistApi = new GitHubGists(User);

        [Test]
        public void ReturnTheSpecifiedGist_WhenTheGistIdentityIsValid_AndTheGistBelongsToTheCurrentUser()
        {
            const int gistIdentity = 1;
            var expectedGist = new Gist(gistIdentity);

            Gist gist = gistApi.Get(gistIdentity);

            Assert.AreEqual(expectedGist.Identity, gist.Identity);
        }

        [Test]
        public void ReturnTheSpecifiedGist_WhenTheGistIdentityIsValid_AndTheGistBelongsToAnotherUser_AndTheGistIsPublic()
        {
            const int gistIdentity = 2;
            var expectedGist = new Gist(gistIdentity);

            Gist gist = gistApi.Get(gistIdentity);

            Assert.AreEqual(expectedGist.Identity, gist.Identity);
        }

        [Test]
        public void ThrowAnArgumentException_WhenTheGistIdentityIsNegative()
        {
            Assert.Throws<ArgumentException>(() => gistApi.Get(-1));
        }

        [Test]
        public void ThrowAGistNotFoundException_WhenTheGistIdentityIsZero()
        {
            Assert.Throws<GistNotFoundException>(() => gistApi.Get(0));
        }

        [Test]
        public void ThrowAGistNotFoundException_WithTheGivenGistIdentity_WhenTheGistIdentityIsZero()
        {
            const int gistIdentity = 0;
            try
            {
                gistApi.Get(gistIdentity);
            }
            catch (GistNotFoundException ex)
            {
                Assert.AreEqual(gistIdentity, ex.Identity);
            }
        }

        [Test]
        public void ThrowAGistNotFoundException_WhenTheGistIdentityIsValid_AndTheGistBelongsToAnotherUser_AndTheGistIsPrivate()
        {
            const int gistIdentity = 3;

            Assert.Throws<GistNotFoundException>(() => gistApi.Get(gistIdentity));
        }
    }
}