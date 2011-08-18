using System.Windows;

namespace GistEd.Debugging.RestClient.Behaviours
{
    public static class DialogCloser
    {
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached("DialogResult", typeof (bool?), typeof (DialogCloser), new PropertyMetadata(DialogResultChanged));

        public static void SetDialogResult(Window target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }

        public static bool? GetDialogResult(Window target)
        {
            return (bool?) target.GetValue(DialogResultProperty);
        }

        private static void DialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window != null)
            {
                window.DialogResult = e.NewValue as bool?;
            }
        }
    }
}
