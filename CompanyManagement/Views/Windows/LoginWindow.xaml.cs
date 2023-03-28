﻿using System.Windows;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.Windows;

/// <summary>
///     Interaction logic for LoginWindow.xaml
/// </summary>
public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
        DataContext = new LoginViewModel(new EmployeeAccountDao());
    }
}