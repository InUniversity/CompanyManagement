using CompanyManagement.ViewModels;
using System.Windows.Controls;

namespace CompanyManagement.UserControls;

public partial class EmployeeInputUC : UserControl
{

    public EmployeeInputUC()
    {
        InitializeComponent();
        DataContext = new EmployeeInputViewModel();
    }
}