﻿using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Windows
{
    class EmployeeViewModel : BaseViewModel
    {
        private ContentControl currentChildView = new AssignUC();

        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); } }

        public ICommand ShowAssignView { get; }


        public EmployeeViewModel()
        {
            ShowAssignView = new RelayCommand<object>(ExecuteShowAssignView);
        }

        private void ExecuteShowAssignView(object obj)
        {
            currentChildView.Content = new AssignUC();

        }
    }
}