using GistEd.Debugging.RestClient.Interfaces;
using GistEd.Debugging.RestClient.ViewModels;
using Ninject;

namespace GistEd.Debugging.RestClient.Views
{
    /// <summary>
    /// Interaction logic for LoginWindowView.xaml
    /// </summary>
    public partial class LoginWindowView : ILoginWindowView
    {
        public LoginWindowViewModel ViewModel { get; private set; }

        [Inject]
        public LoginWindowView(LoginWindowViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;
        }
    }
}
