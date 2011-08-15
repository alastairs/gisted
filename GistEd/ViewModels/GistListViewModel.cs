using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GistEd.GitHub;
using ReactiveUI;
using ReactiveUI.Xaml;

namespace GistEd.ViewModels
{
    public class GistListViewModel : ReactiveObject
    {
        private Gist _SelectedGist;
        
        public GistListViewModel(IGitHubGists gists)
        {
            Gists = new ReactiveCollection<Gist>(gists.Get());

            ConfigureAddGistCommand();
            ConfigureEditGistCommand();
        }

        public IEnumerable<Gist> Gists { get; private set; }

        public Gist SelectedGist
        {
            get { return _SelectedGist; }
            set { this.RaiseAndSetIfChanged(x => x.SelectedGist, value); }
        }

        #region Commands

        public ICommand AddGistCommand { get; private set; }

        public ICommand EditGistCommand { get; private set; }

        #endregion

        private void ConfigureAddGistCommand()
        {
            AddGistCommand = new ReactiveCommand();
        }

        private void ConfigureEditGistCommand()
        {
            EditGistCommand = ReactiveCommand.Create(gist => SelectedGist != null && gist == SelectedGist);
            //var canEditGist = this.WhenAny(x => x.Gists, gist => gist == SelectedGist);
            //EditGistCommand = new ReactiveCommand(canEditGist);
        }
    }
}
