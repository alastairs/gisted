using System;
using GistEd.Debugging.RestClient.Interfaces;
using GistEd.Debugging.RestClient.ViewModels;
using Ninject;

namespace GistEd.Debugging.RestClient.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IGitHubRestClientWindow
    {
        private bool authenticating;
        private readonly ILoginWindowView loginWindowView;

        [Inject]
        public MainWindow(MainWindowViewModel viewModel, ILoginWindowView loginWindowView)
        {
            InitializeComponent();

            ViewModel = viewModel;
            DataContext = ViewModel;

            this.loginWindowView = loginWindowView;
        }
        
        private void MainWindow_OnActivated(object sender, EventArgs e)
        {
            if (authenticating)
            {
                return;
            }

            authenticating = true;

            loginWindowView.Owner = this;
            var result = loginWindowView.ShowDialog() ?? false;
            
            authenticating = false;

            if (!result)
            {
                Close();
            }
        }

        #region Implementation of IGitHubRestClientWindow

        public MainWindowViewModel ViewModel { get; private set; }

        #endregion
    }
}
