namespace GistEd.Debugging.RestClient.Interfaces
{
    public interface IView<out TViewModel> where TViewModel: class
    {
        TViewModel ViewModel { get; }
    }
}