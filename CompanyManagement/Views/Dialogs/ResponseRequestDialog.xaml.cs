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

namespace CompanyManagement.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for ResponseRequestDialog.xaml
    /// </summary>
    public partial class ResponseRequestDialog : Window
    {
        public ResponseRequestDialog()
        {
            InitializeComponent();
            DataContext = new LeaveInputViewModel();
        }
    }
}
