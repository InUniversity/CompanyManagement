﻿using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Dialogs;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Enums;

namespace CompanyManagement.ViewModels.UserControls
{
    public class LeaveRequestsViewModel : BaseViewModel
    {
        private List<LeaveRequest> leaveRequests;
        public List<LeaveRequest> LeaveRequests 
        { get => leaveRequests; set { leaveRequests = value; OnPropertyChanged(); } }

        private List<LeaveRequest> unapprovedLeaveRequests;
        public List<LeaveRequest> UnapprovedLeaveRequests 
        { get => unapprovedLeaveRequests; set { unapprovedLeaveRequests = value; OnPropertyChanged(); } }

        private List<LeaveRequest> approvedLeaveRequests;
        public List<LeaveRequest> ApprovedLeaveRequests 
        { get => approvedLeaveRequests; set { approvedLeaveRequests = value; OnPropertyChanged(); } }

        private List<LeaveRequest> deniedLeaveRequests;
        public List<LeaveRequest>  DeniedLeaveRequests 
        { get => deniedLeaveRequests; set { deniedLeaveRequests = value; OnPropertyChanged(); } }
        
        private DateTime timeCreateLeave = DateTime.Now;
        public DateTime TimeCreateLeave { get => timeCreateLeave; set { timeCreateLeave = value; OnPropertyChanged(); SearchDate(); } }

        public ICommand BackDateCommand { get; private set; }
        public ICommand NextDateCommand { get; private set; }
        public ICommand OpenAddLeaveDialogCommand { get; private set; }
        public ICommand DeleteLeaveCommand { get; private set; }
        public ICommand OpenUpdateLeaveDialogCommand { get; private set; }

        private LeaveRequestsDao leaveDao = new LeaveRequestsDao();
        private EmployeesDao employeeDao = new EmployeesDao();

        private Employee currentEmployee = CurrentUser.Ins.Empl;

        public LeaveRequestsViewModel()
        {
            SetCommands();
            SearchDate();
        }

        private void SetCommands()
        {
            BackDateCommand = new RelayCommand<object>(ExecuteBackTimeLeaveCreateDate);
            NextDateCommand = new RelayCommand<object>(ExecuteNextTimeLeaveCreateDate);
            OpenAddLeaveDialogCommand = new RelayCommand<object>(ExecuteAddCommand);
            DeleteLeaveCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            OpenUpdateLeaveDialogCommand = new RelayCommand<LeaveRequest>(ExecuteUpdateCommand);
        }

        private void ExecuteBackTimeLeaveCreateDate(object obj)
        {
            TimeCreateLeave = timeCreateLeave.AddDays(-1);
        }

        private void ExecuteNextTimeLeaveCreateDate(object obj)
        {
            TimeCreateLeave = timeCreateLeave.AddDays(1);
        }

        private void LoadLeaveRequestList()
        {
            LeaveRequests = leaveDao.GetMyRequests(currentEmployee.ID);

            var receivedLeaveRequests = (currentEmployee.EmplRole.Perms == EPermission.HR)
                ? leaveDao.SearchByApproverID(currentEmployee.ID)
                : leaveDao.GetMyRequests(currentEmployee.ID);
            foreach (LeaveRequest leave in receivedLeaveRequests)
            {
                leave.Approver = employeeDao.SearchByID(leave.ApproverID);
            }

            var unapprovedLeaveList = receivedLeaveRequests
                .Where(p => p.Status == ELeavStatus.Unapproved)
                .OrderByDescending(p => p.Created).ToList();
            UnapprovedLeaveRequests = unapprovedLeaveList;

            var listApprovedLeaves = receivedLeaveRequests
                .Where(p => p.Status == ELeavStatus.Approved) 
                .OrderByDescending(p => p.Created).ToList();
            ApprovedLeaveRequests = listApprovedLeaves;

            var listUnapprovedLeaves = receivedLeaveRequests
                .Where(p => p.Status == ELeavStatus.Denied) 
                .OrderByDescending(p => p.Created).ToList();
            DeniedLeaveRequests = listUnapprovedLeaves;
        }

        private void SearchDate()
        {
            LoadLeaveRequestList();
            var allItem = LeaveRequests;
            allItem = LeaveRequests
                    .Where(item => item.Created.Date == TimeCreateLeave.Date)
                    .ToList();
            LeaveRequests = new List<LeaveRequest>(allItem);
            
            Log.Ins.Information(nameof(LeaveRequestsViewModel), "selected date = " + timeCreateLeave.ToShortDateString());
        }

        private LeaveRequest CreateLeave()
        {
            return new LeaveRequest(AutoGenerateID(), "", "", ELeavStatus.Unapproved, 
                currentEmployee.ID, "", "");
        }

        private string AutoGenerateID()
        {
            string leaaveID;
            Random random = new Random();
            do
            {
                int number = random.Next(10000);
                leaaveID = $"LEA{number:0000}";
            } while (leaveDao.SearchByID(leaaveID) != null);
            return leaaveID;
        }

        private void ExecuteAddCommand(object p)
        {
            var leave = CreateLeave();
            var inputDialogService = new InputDialogService<LeaveRequest>(new AddLeaveDialog(), leave, Add);
            inputDialogService.Show();
            SearchDate();
        }

        private void Add(LeaveRequest leave)
        {
            leaveDao.Add(leave);
            LoadLeaveRequestList();
        }

        private void ExecuteDeleteCommand(string id)
        {
            var dialog = new AlertDialogService( 
                "Xóa xin phép nghỉ", 
                "Bạn chắc chắn muốn xóa xin phép nghỉ!",
                () =>
                {
                    leaveDao.Delete(id); 
                    LoadLeaveRequestList();
                }, null); 
            dialog.Show();
        }

        private void ExecuteUpdateCommand(LeaveRequest leave)
        {
            var inputDialogService = new InputDialogService<LeaveRequest>(new UpdateLeaveDialog(), leave, Update);
            inputDialogService.Show();
        }
        
        private void Update(LeaveRequest leave)
        {
            leaveDao.Update(leave);
            LoadLeaveRequestList();
        }
    }
}
