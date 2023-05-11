using System;
using System.Windows.Controls;
using CompanyManagement.Models;
using CompanyManagement.Strategies.UserControls.ProjectsView;
using CompanyManagement.Utilities;
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
            try
            {
                var curEmpl = CurrentUser.Ins.EmployeeIns;
                var projectsStrategy = ProjectsStrategyFactory.Create(curEmpl.EmplRole.Perms);
                DataContext = new ProjectsViewModel(projectsStrategy);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(ProjectsUC), ex.Message);
            }
        }
    }
}