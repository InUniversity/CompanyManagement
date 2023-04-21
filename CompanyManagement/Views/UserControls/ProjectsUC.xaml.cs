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
            var positionID = CurrentUser.Ins.EmployeeIns.PositionID;
            var projectsStrategy = ProjectsStrategyFactory.Create(positionID);
            DataContext = new ProjectsViewModel(projectsStrategy);
        }
    }
}