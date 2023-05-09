using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for AddDepartmentDialog.xaml
    /// </summary>
    public partial class AddDepartmentDialog : Window, IInputDialog<Department>
    {
        public IInputViewModel<Department> ViewModel { get; }
        public AddDepartmentDialog()
        {
            InitializeComponent();
            ViewModel = new AddDepartmentViewModel();
            DataContext = ViewModel;
        }
        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}
