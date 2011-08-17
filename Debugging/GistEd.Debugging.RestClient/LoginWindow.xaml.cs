using System.Windows;
using GistEd.Debugging.RestClient.ViewModels;
using Ninject;

namespace GistEd.Debugging.RestClient
{
    /// <summary>
    /// Interaction logic for LoginWindowView.xaml
    /// </summary>
    public partial class LoginWindowView : ILoginWindowView
    {
        public LoginWindowViewModel ViewModel { get; private set; }

        public GitHubToken Token { get; private set; }

        [Inject]
        public LoginWindowView(LoginWindowViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            Token = ViewModel.Token;
            DialogResult = true;
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Token = null;
            DialogResult = false;
            Close();
        }
    }
}
