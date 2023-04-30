﻿using System.Windows.Controls;
using CompanyManagement.Models;
using CompanyManagement.Strategies.UserControls.LeaveListView;
using CompanyManagement.Strategies.UserControls.ProjectsView;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for LeaveUC.xaml
    /// </summary>
    public partial class LeaveRequestsUC : UserControl
    {
        public LeaveRequestsUC()
        {
            InitializeComponent();
            var positionID = CurrentUser.Ins.EmployeeIns.RoleID;
            var leaveListStrategy = LeaveListStrategyFactory.Create(positionID);
            DataContext = new LeaveRequestsViewModel(leaveListStrategy);
        }
    }
}