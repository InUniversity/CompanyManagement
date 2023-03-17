using CompanyManagement.ViewModels;
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

namespace CompanyManagement.Dialogs
{
    /// <summary>
    /// Interaction logic for AddProjectDialog.xaml
    /// </summary>
    public partial class AddProjectDialog : Window
    {
        public AddProjectDialog()
        {
            InitializeComponent();
            DataContext = new AddProjectViewModel();
            projectInputUC.DataContext = new ProjectInputViewModel();
            ((AddProjectViewModel)DataContext).ProjectInputDataContext=(ProjectInputViewModel)projectInputUC.DataContext;
        }
    }
}
