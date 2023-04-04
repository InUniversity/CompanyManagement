﻿using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ProjectDetailsUC.xaml
    /// </summary>
    public partial class ProjectDetailsUC : UserControl
    {
        public ProjectDetailsUC()
        {
            InitializeComponent();
            DataContext = new ProjectDetailsViewModel();
        }
    }
}