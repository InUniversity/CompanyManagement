using CompanyManagement.ViewModels;
using System.Windows;

namespace CompanyManagement.Dialogs
{
    /// <summary>
    /// Interaction logic for AddTaskInProject.xaml
    /// </summary>
    public partial class AddTaskDialog : Window
    {
        public AddTaskDialog()
        {
            InitializeComponent();
            DataContext = new AddTaskViewModel();
        }
    }
}
