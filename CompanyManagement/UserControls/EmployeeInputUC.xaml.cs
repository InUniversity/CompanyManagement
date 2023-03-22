using System.Windows.Controls;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels;

namespace CompanyManagement.UserControls;

public partial class EmployeeInputUC : UserControl
{
    public EmployeeInputUC()
    {
        InitializeComponent();
        DataContext = new EmployeeInputViewModel(new PositionDao(), new DepartmentDao());
    }
}