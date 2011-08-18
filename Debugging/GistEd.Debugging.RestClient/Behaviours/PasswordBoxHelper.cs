using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GistEd.Debugging.RestClient.Behaviours
{
    /// <summary>
    /// Simplified version of Sam Jack's attached property available on his blog.
    /// http://blog.functionalfun.net/2008/06/wpf-passwordbox-and-data-binding.html
    /// </summary>
    public static class PasswordBoxHelper
    {
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached("BoundPassword", typeof (string), typeof (PasswordBoxHelper), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        private static readonly DependencyProperty UpdatingPasswordProperty =
            DependencyProperty.RegisterAttached("UpdatingPassword", typeof (bool), typeof (PasswordBoxHelper), new PropertyMetadata(false));

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as PasswordBox;

            if (box == null)
            {
                return;
            }

            box.PasswordChanged -= HandlePasswordChanged;

            var newPassword = (string) e.NewValue;

            if (!GetUpdatingPassword(box))
            {
                box.Password = newPassword;
            }

            box.PasswordChanged += HandlePasswordChanged;
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as PasswordBox;

            SetUpdatingPassword(box, true);

            SetBoundPassword(box, box.Password);
            SetUpdatingPassword(box, false);
        }

        public static void SetUpdatingPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(UpdatingPasswordProperty, value);
        }

        public static bool GetUpdatingPassword(DependencyObject dp)
        {
            return (bool) dp.GetValue(UpdatingPasswordProperty);
        }

        public static void SetBoundPassword(DependencyObject dp, string value)
        {
            dp.SetValue(BoundPasswordProperty, value);
        }

        public static string GetBoundPassword(DependencyObject dp)
        {
            return (string) dp.GetValue(BoundPasswordProperty);
        }
    }
}
