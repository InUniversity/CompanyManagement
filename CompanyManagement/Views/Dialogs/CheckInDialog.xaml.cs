using CompanyManagement.ViewModels.UserControls;
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
using CompanyManagement.ViewModels.Dialogs.Interfaces;

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for CheckInDialog.xaml
    /// </summary>
    public partial class CheckInDialog : Window
    {
        public CheckInViewModel ViewModel { get; }

        public CheckInDialog()
        {
            InitializeComponent();
            ViewModel = new CheckInViewModel();
            DataContext = ViewModel; 
        }
    }
}
