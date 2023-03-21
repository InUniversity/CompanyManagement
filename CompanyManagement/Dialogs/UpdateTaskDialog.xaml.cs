using CompanyManagement.ViewModels;
using System.Windows;

namespace CompanyManagement.Dialogs
{
    /// <summary>
    /// Interaction logic for UpdateTaskInProject.xaml
    /// </summary>
    public partial class UpdateTaskDialog : Window
    {
        public UpdateTaskDialog()
        {
            InitializeComponent();
            DataContext = new UpdateTaskViewModel();
        }
    }
}
