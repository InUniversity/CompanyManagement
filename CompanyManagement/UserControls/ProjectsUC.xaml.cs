﻿using System.Windows.Controls;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels;

namespace CompanyManagement.UserControls;

/// <summary>
///     Interaction logic for _ProjectUC.xaml
/// </summary>
public partial class ProjectsUC : UserControl
{
    public ProjectsUC()
    {
        InitializeComponent();
        DataContext = new ProjectsViewModel(new ProjectDao());
        ((ProjectsViewModel)DataContext).TasksDataContext = 
            new TasksInProjectViewModel(new TaskInProjectDao(), new ProjectAssignmentDao());
    }
}