using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ViewPersonalSalariesUC.xaml
    /// </summary>
    public partial class PersonalSalaryUC : UserControl
    {
        public PersonalSalaryUC()
        {
            InitializeComponent();
            DataContext = new PersonalSalaryViewModel();
        }
    }
}
