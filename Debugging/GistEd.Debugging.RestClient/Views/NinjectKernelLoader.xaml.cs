using System.Threading.Tasks;
using System.Windows;
using GistEd.Debugging.RestClient.Interfaces;
using Ninject;

namespace GistEd.Debugging.RestClient.Views
{
    /// <summary>
    /// Interaction logic for NinjectKernelLoader.xaml
    /// </summary>
    public partial class NinjectKernelLoader
    {
        protected IKernel Kernel;

        public NinjectKernelLoader()
        {
            InitializeComponent();
            InitializeKernel();
        }

        private void InitializeKernel()
        {
            // Create a scheduler for the WPF Dispatcher.
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory.StartNew(() =>
                                      {
                                          Kernel = new StandardKernel();
                                          Kernel.Load<GitHubRestClientAppModule>();
                                      })
                        .ContinueWith(task =>
                                          {
                                              Hide();
                                              if (Kernel.Get<IScreenFactory>().GetLoginWindow().ShowDialog() == false)
                                              {
                                                  Application.Current.Shutdown();
                                              }
                                              Kernel.Get<IScreenFactory>().GetMainWindow().Show();
                                          }, scheduler);
        }
    }
}
