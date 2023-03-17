﻿using CompanyManagement.ViewModels;
using System.Windows;

namespace CompanyManagement.Dialogs
{
    /// <summary>
    /// Interaction logic for UpdateEmployeeDialog.xaml
    /// </summary>
    public partial class UpdateEmployeeDialog : Window
    {

        public UpdateEmployeeDialog()
        {
            InitializeComponent();
            DataContext = new UpdateEmployeeViewModel();
        }
    }
}
