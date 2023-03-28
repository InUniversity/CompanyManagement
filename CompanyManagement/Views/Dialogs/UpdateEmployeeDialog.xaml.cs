﻿using System.Windows;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.Dialogs;

/// <summary>
///     Interaction logic for UpdateEmployeeDialog.xaml
/// </summary>
public partial class UpdateEmployeeDialog : Window
{
    public UpdateEmployeeDialog()
    {
        InitializeComponent();
        IEmployeeInput employeeInput = new EmployeeInputViewModel(new PositionDao(), new DepartmentDao());
        DataContext = new UpdateEmployeeViewModel(employeeInput);
    }
}