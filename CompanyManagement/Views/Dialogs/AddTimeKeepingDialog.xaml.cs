using System.Windows;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.Dialogs;

/// <summary>
/// Interaction logic for AddTimeKeepingDialog.xaml
/// </summary>
public partial class AddTimeKeepingDialog : Window
{
    public AddTimeKeepingDialog()
    {
        InitializeComponent();
        ITimeKeepingInput timeKeepingInput = new TimeKeepingInputViewModel();
        DataContext = new AddTimeKeepingViewModel();
    }
}
