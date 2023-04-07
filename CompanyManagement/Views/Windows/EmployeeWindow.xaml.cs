using System.Windows;
using CompanyManagement.ViewModels.Windows;

namespace CompanyManagement.Views.Windows
{
    /// <summary>
    ///     Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
            DataContext = new EmployeeViewModel();
        }
    }
}