using System.Windows;

namespace GistEd.Debugging.RestClient.Interfaces
{
    /// <summary>
    /// Wrapper interface for <see cref="Window"/>.
    /// </summary>
    public interface IWindow
    {
        void Show();
        void Close();
        WindowStartupLocation WindowStartupLocation { get; set; }
        Window Owner { get; set; }
        bool? ShowDialog();
    }
}