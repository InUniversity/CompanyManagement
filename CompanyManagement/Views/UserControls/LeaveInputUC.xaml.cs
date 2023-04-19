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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CompanyManagement.ViewModels;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for LeaveInputUC.xaml
    /// </summary>
    public partial class LeaveInputUC : UserControl
    {
        public LeaveInputUC()
        {
            InitializeComponent();
            DataContext = new LeaveInputViewModel();
        }
    }
}
