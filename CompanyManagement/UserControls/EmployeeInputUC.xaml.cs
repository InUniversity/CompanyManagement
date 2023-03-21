using CompanyManagement.ViewModels;
using System.Windows.Controls;
using CompanyManagement.Database.Implementations;

namespace CompanyManagement.UserControls;

public partial class EmployeeInputUC : UserControl
{

    public EmployeeInputUC()
    {
        InitializeComponent();
        DataContext = new EmployeeInputViewModel(new PositionDao(), new DepartmentDao());
    }
}