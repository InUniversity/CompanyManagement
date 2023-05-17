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
            IProjectsStrategy strategy = new ProjectsForEmployee();
            try
            {
                var curEmpl = CurrentUser.Ins.Empl;
                strategy = ProjectsStrategyFactory.Create(curEmpl.EmplRole.Perms);
            }
            catch (Exception ex)
            {
                Log.Ins.Error(nameof(ProjectsUC), ex.Message);
            }
            DataContext = new ProjectsViewModel(strategy);
        }
    }
}