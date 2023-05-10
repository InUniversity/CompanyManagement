using CompanyManagement.ViewModels.Dialogs;
using System.Windows;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for ViewDetailsSalaryRecordDialog.xaml
    /// </summary>
    public partial class SalaryDetailsDialog : Window
    {
        public SalaryDetailsDialog()
        {
            InitializeComponent();
            DataContext = new SalaryDetailViewModel();
        }
    }
}
