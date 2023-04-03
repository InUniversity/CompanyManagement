using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using CompanyManagement.Database.Implementations;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls;

/// <summary>
///     Interaction logic for ProjectInputUC.xaml
/// </summary>
public partial class ProjectInputUC : UserControl
{
    public ProjectInputUC()
    {
        InitializeComponent();
        DataContext = new ProjectInputViewModel(new ProjectAssignmentDao(), new ProjectStatusDao());

    }
}