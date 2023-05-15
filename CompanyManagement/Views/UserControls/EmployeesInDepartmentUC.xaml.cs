using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for EmployeesInDepartmentUC.xaml
    /// </summary>
    public partial class EmployeesInDepartmentUC : UserControl
    {
        public EmployeesInDepartmentUC()
        {
            InitializeComponent();
            DataContext = new EmployeesInDepartmentViewModel();
        }
    }
}
