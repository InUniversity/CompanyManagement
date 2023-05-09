using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public class MilestonesViewModel : BaseViewModel, IRetrieveProjectID
    {

        private List<Milestone> allMilestones = new List<Milestone>();

        private ObservableCollection<Milestone> milestones = new ObservableCollection<Milestone>();
        public ObservableCollection<Milestone> Milestones { get => milestones; set { milestones = value; OnPropertyChanged(); } } 

        private string projectID = "";

        public ICommand OpenAddMilestoneDialogCommand { get; private set; }
        public ICommand DeleteMilestoneCommand { get; private set; }
        public ICommand OpenUpdateMilestoneDialogCommand { get; private set; }

        private MilestonesDao milestonesDao = new MilestonesDao();
        private MileTasksDao mileTasksDao = new MileTasksDao();
        private EmployeesDao employeesDao = new EmployeesDao();

        public MilestonesViewModel()
        {
            SetCommads();
        }

        private void SetCommads()
        {
            OpenAddMilestoneDialogCommand = new RelayCommand<object>(ExcuteOpenAddMilestoneDialog);
            DeleteMilestoneCommand = new RelayCommand<string>(ExcuteDeleteMilestone);
            OpenUpdateMilestoneDialogCommand = new RelayCommand<Milestone>(ExcuteOpenUpdateMilestoneDialog);
        }

        private void LoadAllMilestoneList()
        {
            allMilestones = milestonesDao.SearchByProjectID(projectID).OrderByDescending(p => p.End).ToList();
            foreach(Milestone milestone in allMilestones)
            {
                var searchedtasksInMile = mileTasksDao.SearchByMileID(milestone.ID);
                milestone.TasksInMile = new ObservableCollection<TaskInProject>(searchedtasksInMile);
                milestone.Owner = employeesDao.SearchByID(milestone.OwnerID);
                milestone.Progress = CalculatePercentCompleted(milestone);
                milestone.TimeUntilDl = Convert.ToInt32(milestone.End.Subtract(DateTime.Now).Days);
            }    
        }

        private int CalculatePercentCompleted(Milestone milestone)
        {
            int numberTask = milestone.TasksInMile.Count;
            if (numberTask == 0) return 0;
            int numberTaskCompleted = mileTasksDao.SearchTaskCompletedByMileID(milestone.ID).Count;
            return (int)(numberTaskCompleted * 100) / numberTask;
        }

        private void LoadAllMilestones()
        {
            LoadAllMilestoneList();
            Milestones = new ObservableCollection<Milestone>(allMilestones);
        }

        private void ExcuteOpenAddMilestoneDialog(object obj)
        {
            var milestone = CreateMilestone();
            var inputService = new InputDialogService<Milestone>(new AddMilestoneDialog(), milestone, AddToDB);
            inputService.Show();
        }

        private Milestone CreateMilestone()
        {
            return new Milestone(AutoGenerateID(), "", "", Utils.emptyDate, Utils.emptyDate,
                 DateTime.Now, CurrentUser.Ins.EmployeeIns.ID, projectID);
        }

        private string AutoGenerateID()
        {
            string milestoneID;
            Random random = new Random();
            do
            {
                int number = random.Next(10000);
                milestoneID = $"MILE{number:0000}"; 
            } while (milestonesDao.SearchByID(milestoneID) != null);
            return milestoneID;
        }

        private void AddToDB(Milestone milestone)
        {
            milestonesDao.Add(milestone);
            AddMileTaskToDB(milestone);
            LoadAllMilestones();
        }

        private void ExcuteDeleteMilestone(string id)
        {
            AlertDialogService dialog = new AlertDialogService(
               "Xóa cột mốc",
               "Bạn chắc chắn muốn xóa cột mốc cho dự án !",
               () =>
               {
                   DeleteMileTaskInDB(id);
                   milestonesDao.Delete(id);
                   LoadAllMilestones();
               }, null);
            dialog.Show();
        }

        private void ExcuteOpenUpdateMilestoneDialog(Milestone milestone)
        {
            List<TaskInProject> taskBeforeChange = mileTasksDao.SearchByMileID(milestone.ID);
            milestone.TasksInMile = new ObservableCollection<TaskInProject>(taskBeforeChange);  
            var inputService = new InputDialogService<Milestone>(new UpdateMilestoneDialog(), milestone, UpdateToDB);
            inputService.Show();
        }

        private void UpdateToDB(Milestone milestone)
        {
            milestonesDao.Update(milestone);
            DeleteMileTaskInDB(milestone.ID);
            AddMileTaskToDB(milestone);
            LoadAllMilestones();
        }

        private void DeleteMileTaskInDB(string id)
        {
            mileTasksDao.DeleteByMileID(id);
        }

        private void AddMileTaskToDB(Milestone milestone)
        {
            foreach (TaskInProject task in milestone.TasksInMile)
            {
                MileTask mileTask = new MileTask(milestone.ID, task.ID);
                mileTasksDao.Add(mileTask);
            }
        }

        public void ReceiveProjectID(string projectID)
        {
            this.projectID = projectID;
            LoadAllMilestones();
        }
    }
}
