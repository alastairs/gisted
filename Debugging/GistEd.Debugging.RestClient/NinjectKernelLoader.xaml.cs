using System.Threading.Tasks;
using Ninject;

namespace GistEd.Debugging.RestClient
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
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Factory.StartNew(() =>
                                      {
                                          Kernel = new StandardKernel();
                                          Kernel.Load<GitHubRestClientAppModule>();
                                      })
                        .ContinueWith(task =>
                                          {
                                              Hide();
                                              Kernel.Get<IGitHubRestClientWindow>().Show();
                                              Close();
                                          }, scheduler);
        }
    }
}
