using System.Collections.Generic;
using GistEd.GitHub;
using GistEd.ViewModels;
using Moq;
using NUnit.Framework;

namespace GistEd.Tests
{
    [TestFixture]
    public class GistListViewModelEditGistCommandShould
    {
        [Test]
        public void BeExecutable_WhenAGistIsSelected()
        {
            var gist = new Gist();
            var gists = new Mock<IGitHubGists>();
            gists.Setup(g => g.Get()).Returns(new List<Gist> {gist});

            var gistListViewModel = new GistListViewModel(gists.Object)
                                        {
                                            SelectedGist = gist
                                        };

            Assert.IsTrue(gistListViewModel.EditGistCommand.CanExecute(gist));
        }

        [Test]
        public void NotBeExecutable_WhenNoGistIsSelected()
        {
            var gists = new GitHubGists("user");
            var gistListViewModel = new GistListViewModel(gists)
                                        {
                                            SelectedGist = null
                                        };

            Assert.IsFalse(gistListViewModel.EditGistCommand.CanExecute(null));
        }
    }
}
