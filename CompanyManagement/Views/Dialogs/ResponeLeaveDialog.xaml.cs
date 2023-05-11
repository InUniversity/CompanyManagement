using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.Dialogs;
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
    /// Interaction logic for ResponeLeaveDialog.xaml
    /// </summary>
    public partial class ResponeLeaveDialog : Window, IInputDialog<LeaveRequest>
    {
        public IInputViewModel<LeaveRequest> ViewModel { get; }

        public ResponeLeaveDialog()
        {
            InitializeComponent();
            ViewModel = new ResponeLeaveViewModel();
            DataContext = ViewModel;
        }

        public void ShowInputDialog()
        {
            ShowDialog();
        }
    }
}
