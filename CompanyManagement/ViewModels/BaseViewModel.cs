using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, ICloseWindows
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand? closeCommand;
        public ICommand CloseCommand => closeCommand ?? (closeCommand = new ReplayCommand<object>(CloseWindow));

        public Action Close { get; set; }

        public virtual bool CanClose()
        {
            return true;
        }

        protected virtual void CloseWindow(object p)
        {
            Close?.Invoke();
        }
    }

    interface ICloseWindows
    {
        Action Close { get; set; }
        bool CanClose();
    }

    public class WindowCloser
    {

        public static bool GetEnableWindowClosing(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableWindowClosingProperty);
        }

        public static void SetEnableWindowClosing(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableWindowClosingProperty, value);
        }

        // Using a DependencyProperty as the backing store for EnableWindowClosing.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableWindowClosingProperty =
            DependencyProperty.RegisterAttached("EnableWindowClosing", typeof(bool), typeof(WindowCloser), new PropertyMetadata(false, OnEnableWindowClosingChanged));

        private static void OnEnableWindowClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                window.Loaded += (s, e) =>
                {
                    if (window.DataContext is ICloseWindows vm)
                    {
                        vm.Close += () =>
                        {
                            window.Close();
                        };

                        window.Closing += (s, e) =>
                        {
                            e.Cancel = !vm.CanClose();
                        };
                    }
                };
            }
        }
    }
}
