using System.Windows.Controls;
using CompanyManagement.Models;
using CompanyManagement.Strategies.UserControls.ProjectsView;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    ///     Interaction logic for _ProjectUC.xaml
    /// </summary>
    public partial class ProjectsUC : UserControl
    {
        public ProjectsUC()
        {
            InitializeComponent();
            var roleID = CurrentUser.Ins.EmployeeIns.PermsID;
            var projectsStrategy = ProjectsStrategyFactory.Create(roleID);
            DataContext = new ProjectsViewModel(projectsStrategy);
        }
    }
}