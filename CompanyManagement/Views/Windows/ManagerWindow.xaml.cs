using System.Windows;
using CompanyManagement.ViewModels.Windows;

namespace CompanyManagement.Views.Windows;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class ManagerWindow : Window
{
    public ManagerWindow()
    {
        InitializeComponent();
        DataContext = new ManagerViewModel();
    }
}