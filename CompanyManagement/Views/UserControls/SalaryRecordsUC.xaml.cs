using CompanyManagement.ViewModels.UserControls;
using System;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for SalaryRecordsUC.xaml
    /// </summary>
    public partial class SalaryRecordsUC : UserControl
    {
        public SalaryRecordsUC()
        {
            InitializeComponent();
            DataContext = new SalaryRecordsViewModel();
        }
    }
}
