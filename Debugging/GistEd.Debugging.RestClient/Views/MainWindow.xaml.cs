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
        [Inject]
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            DataContext = ViewModel;
        }

        #region Implementation of IGitHubRestClientWindow

        public MainWindowViewModel ViewModel { get; private set; }

        #endregion
    }
}
