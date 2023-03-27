using CompanyManagement.ViewModels;
using System.Windows;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System;

namespace CompanyManagement.Views.Windows;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class ManagerWindow : Window
{
    public ManagerWindow()
    {
        InitializeComponent();
        DataContext = new ManagerWindowViewModel();
    }
}