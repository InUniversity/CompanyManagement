using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CompanyManagement.Services;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows;
using CompanyManagement.Utilities;
using System.ComponentModel;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Database.Base;
using CompanyManagement.Views.UserControls;

namespace CompanyManagement.ViewModels.UserControls
{
    public class ProjectBonusViewModel : BaseViewModel, IRetrieveProjectID
    {
        private ObservableCollection<ProjectBonus> bonusNormalList = new ObservableCollection<ProjectBonus>();
        public ObservableCollection<ProjectBonus> BonusNormalList
        { get => bonusNormalList; set { bonusNormalList = value; OnPropertyChanged(); } }

        private ObservableCollection<ProjectBonus> nonBonusList = new ObservableCollection<ProjectBonus>();
        public ObservableCollection<ProjectBonus> NonBonusList 
        { get => nonBonusList; set { nonBonusList = value; OnPropertyChanged(); } }

        private ObservableCollection<ProjectBonus> bonusSpecial = new ObservableCollection<ProjectBonus>();
        public ObservableCollection<ProjectBonus> BonusSpecial 
        { get => bonusSpecial; set { bonusSpecial = value; OnPropertyChanged(); } }

        private Project currentProject;

        private string projectID = "";
        private decimal BonusSalary = 0;
        private int percentRemaining = 0;

        private ProjectAssignmentsDao projectAssignmentDao = new ProjectAssignmentsDao();
        private ProjectsDao projectsDao = new ProjectsDao();
        private ProjectBonusesDao projectBonusesDao = new ProjectBonusesDao();

        public ICommand AddBonusCommand { get; private set; }
        public ICommand AddNonBonusCommand { get; private set; }
        public ICommand AddCalculateShareBonusCommand { get; private set; }
        public ICommand RestoreBonusCommand { get; private set; }
        public ICommand RestoreNonBonusCommand { get; private set; }
        public ICommand SaveShareBonusToDBCommand { get; private set; }

        public ProjectBonusViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            AddBonusCommand = new RelayCommand<ProjectBonus>(ExecuteAddBonusCommand);
            AddNonBonusCommand = new RelayCommand<ProjectBonus>(ExecuteAddNonBonusCommand);
            AddCalculateShareBonusCommand = new RelayCommand<ProjectBonus>(ExecuteCalculateShareBonusCommand);
            RestoreBonusCommand = new RelayCommand<ProjectBonus>(ExecuteRestoreBonusCommand);
            RestoreNonBonusCommand = new RelayCommand<ProjectBonus>(ExecuteRestoreNonBonusCommand);
            SaveShareBonusToDBCommand = new RelayCommand<object>(ExecuteAddShareBonusToDBCommand);
        }

        private void SetProjectBonuses()
        {
            BonusNormalList = new ObservableCollection<ProjectBonus>(CreateItemInNormal());
            percentRemaining = LoadNextPercentRemaining(); 
            LoadAmountInBonuses();
        }

        private List<ProjectBonus> CreateItemInNormal()
        {
            var employeesInProject = projectAssignmentDao.GetEmployeesInProject(projectID);
            List<ProjectBonus> createList = new List<ProjectBonus>();
            foreach (Employee emp in employeesInProject)
            {
                ProjectBonus projectBonuses = new ProjectBonus((new Random()).Next(999).ToString(),
                    0, DateTime.Now, emp.ID, projectID);
                projectBonuses.Receiver = emp;
                createList.Add(projectBonuses);
            }
            return createList;
        }

        private void LoadAmountInBonuses()
        {
            if (bonusSpecial.Count != 0)
            BonusSpecial = new ObservableCollection<ProjectBonus>(UpdateAmountInSpecical());

            if (bonusNormalList.Count != 0)
            BonusNormalList = new ObservableCollection<ProjectBonus>(UpdateAmountInNormal());
        }

        private List<ProjectBonus> UpdateAmountInSpecical()
        {
            List<ProjectBonus> updateList = new List<ProjectBonus>();
            foreach (ProjectBonus projectBonuses in BonusSpecial)
            {
                updateList.Add(projectBonuses);
            }
            return updateList;
        }

        private List<ProjectBonus> UpdateAmountInNormal()
        {
            List<ProjectBonus> updateList = new List<ProjectBonus>();
            decimal amount = CalculateEachAmountOfList(bonusNormalList.Count());
            foreach (ProjectBonus projectBonuses in BonusNormalList)
            {
                projectBonuses.Amount = amount;
                updateList.Add(projectBonuses);
            }
            return updateList;
        }

        private decimal CalculateEachAmountOfList(int count)
        {
            return (decimal)(percentRemaining / count) * BonusSalary;
        }

        private string AutoGenerateID()
        {
            string projectBonusID;
            Random random = new Random();
            do
            {
                int number = random.Next(100000);
                projectBonusID = $"PB{number:00000}";
            } while (projectBonusesDao.SearchByID(projectBonusID) == null);
            return projectBonusID;
        }

        private void ExecuteAddBonusCommand(ProjectBonus projectBonus)
        {
            bonusNormalList.Remove(projectBonus);
            projectBonus.Amount = 0;
            bonusSpecial.Add(projectBonus);
            LoadAmountInBonuses();
        }

        private void ExecuteAddNonBonusCommand(ProjectBonus projectBonus)
        {
            nonBonusList.Add(projectBonus);
            bonusNormalList.Remove(projectBonus);
            LoadAmountInBonuses();
        }

        private void ExecuteCalculateShareBonusCommand(ProjectBonus projectBonus)
        {
            if (!ValidatePercent(projectBonus)) return;
            projectBonus.Amount = CalculateAmountOfProject(projectBonus.Percent);
            LoadAmountInBonuses();
        }

        private decimal CalculateAmountOfProject(int percent)
        {
            return (decimal)(percent * BonusSalary) / 100;
        }

        private int LoadNextPercentRemaining()
        {
            int totalPercent = 100;
            foreach (ProjectBonus projectBonuses in BonusSpecial)
            {
                totalPercent -= projectBonuses.Percent;
            }
            return totalPercent;
        }

        private bool ValidatePercent(ProjectBonus projectBonus)
        {
            int newPercent = LoadNextPercentRemaining();
            if (newPercent < 0)
            {
                projectBonus.Percent = 0;
                var dialog = new AlertDialogService(
                "Nhập liệu sai",
                "Tổng mức thưởng thiết lập quá 100%", null, null);
                dialog.Show();
                return false;
            }
            percentRemaining = newPercent;
            return true;
        }

        private void ExecuteRestoreBonusCommand(ProjectBonus projectBonus)
        {
            bonusNormalList.Add(projectBonus);
            bonusSpecial.Remove(projectBonus);
            percentRemaining = LoadNextPercentRemaining();
            LoadAmountInBonuses();
        }

        private void ExecuteRestoreNonBonusCommand(ProjectBonus projectBonus)
        {

            bonusNormalList.Add(projectBonus);
            nonBonusList.Remove(projectBonus);
            LoadAmountInBonuses();
        }

        private void ExecuteAddShareBonusToDBCommand(object obj)
        {
            if (!ValidateShareBonus()) return;
            var dialog = new AlertDialogService(
                "Chia tiền thưởng",
                "Bạn chắc chắn muốn lưu các thao tác chia tiền thưởng!",
                () =>
                {
                    CommitShareBonnus();
                }, null);
            dialog.Show();
        }

        private bool ValidateShareBonus()
        {
            if (currentProject.StatusID != BaseDao.projPendingPayID)
            {
                var dialog = new AlertDialogService(
                    "Thông báo",
                    "Trạng thái của dự án chưa thể chia tiền thưởng", null, null);
                dialog.Show();
                return false;
            }
            return true;
        }

        private void CommitShareBonnus()
        {
            projectBonusesDao.DeleteByProjectID(projectID);
            AddShareBonusToDBCommand(bonusNormalList);
            AddShareBonusToDBCommand(bonusSpecial);
            currentProject.ID = BaseDao.projCompletedID;
            projectsDao.Update(currentProject);
        }

        private void AddShareBonusToDBCommand(ObservableCollection<ProjectBonus> list)
        {
            if (list.Count == 0) return;
            foreach(ProjectBonus projectBonuses in list)
            {
                projectBonuses.ID = AutoGenerateID();
                projectBonusesDao.Add(projectBonuses);
            }
        }

        public void ReceiveProjectID(string projectID)
        {
            this.projectID = projectID;
            this.currentProject = projectsDao.SearchByID(projectID);
            this.BonusSalary = currentProject.BonusSalary;
            SetProjectBonuses();
        }
    }
}
