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

namespace CompanyManagement.ViewModels.UserControls
{
    public class ProjectBonusViewModel : BaseViewModel, IRetrieveProjectID
    {
        private ObservableCollection<ProjectBonuses> bonusNormalList = new ObservableCollection<ProjectBonuses>();
        public ObservableCollection<ProjectBonuses> BonusNormalList
        { get => bonusNormalList; set { bonusNormalList = value; OnPropertyChanged(); } }

        private ObservableCollection<ProjectBonuses> nonBonusList = new ObservableCollection<ProjectBonuses>();
        public ObservableCollection<ProjectBonuses> NonBonusList 
        { get => nonBonusList; set { nonBonusList = value; OnPropertyChanged(); } }

        private ObservableCollection<ProjectBonuses> bonusSpecial = new ObservableCollection<ProjectBonuses>();
        public ObservableCollection<ProjectBonuses> BonusSpecial 
        { get => bonusSpecial; set { bonusSpecial = value; OnPropertyChanged(); } }

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
            AddBonusCommand = new RelayCommand<ProjectBonuses>(ExecuteAddBonusCommand);
            AddNonBonusCommand = new RelayCommand<ProjectBonuses>(ExecuteAddNonBonusCommand);
            AddCalculateShareBonusCommand = new RelayCommand<ProjectBonuses>(ExecuteCalculateShareBonusCommand);
            RestoreBonusCommand = new RelayCommand<ProjectBonuses>(ExecuteRestoreBonusCommand);
            RestoreNonBonusCommand = new RelayCommand<ProjectBonuses>(ExecuteRestoreNonBonusCommand);
            SaveShareBonusToDBCommand = new RelayCommand<object>(ExecuteAddShareBonusToDBCommand);
        }

        private void SetProjectBonuese()
        {
            BonusNormalList = new ObservableCollection<ProjectBonuses>(CreateItemInNormal());
            percentRemaining = LoadNextPercentRemaining(); 
            LoadAmountInBonuses();
        }

        private List<ProjectBonuses> CreateItemInNormal()
        {
            var employeesInProject = projectAssignmentDao.GetEmployeesInProject(projectID);
            List<ProjectBonuses> createList = new List<ProjectBonuses>();
            foreach (Employee emp in employeesInProject)
            {
                //Random đại do chưa có add cái nào trong db nên ko dùng AutoGenerateID được
                ProjectBonuses projectBonuses = new ProjectBonuses((new Random()).Next(999).ToString(),
                    0, DateTime.Now, emp.ID, projectID);
                projectBonuses.Receiver = emp;
                createList.Add(projectBonuses);
            }
            return createList;
        }

        private void LoadAmountInBonuses()
        {
            if (bonusSpecial.Count == 0) return;
            BonusSpecial = new ObservableCollection<ProjectBonuses>(UpdateAmountInSpecical());

            if (bonusNormalList.Count == 0) return;
            BonusNormalList = new ObservableCollection<ProjectBonuses>(UpdateAmountInNormal());
        }

        private int LoadNextPercentRemaining()
        {
            int totalPercent = 100;
            foreach (ProjectBonuses projectBonuses in BonusSpecial)
            {
                totalPercent -= projectBonuses.Percent;
            }
            return totalPercent;
        }

        private List<ProjectBonuses> UpdateAmountInNormal()
        {
            List<ProjectBonuses> updateList = new List<ProjectBonuses>();
            MessageBox.Show(percentRemaining.ToString());
            decimal amount = (decimal)(percentRemaining / BonusNormalList.Count()) * BonusSalary;
            foreach (ProjectBonuses projectBonuses in BonusNormalList)
            {
                projectBonuses.Amount = amount;
                updateList.Add(projectBonuses);
            }
            return updateList;
        }

        private List<ProjectBonuses> UpdateAmountInSpecical()
        {
            List<ProjectBonuses> updateList = new List<ProjectBonuses>();
            foreach (ProjectBonuses projectBonuses in BonusSpecial)
            {
                updateList.Add(projectBonuses);
            }
            return updateList;
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

        private void ExecuteAddBonusCommand(ProjectBonuses projectBonus)
        {
            bonusNormalList.Remove(projectBonus);
            projectBonus.Amount = 0;
            bonusSpecial.Add(projectBonus);
            LoadAmountInBonuses();
        }

        private void ExecuteAddNonBonusCommand(ProjectBonuses projectBonus)
        {
            nonBonusList.Add(projectBonus);
            bonusNormalList.Remove(projectBonus);
            LoadAmountInBonuses();
        }

        private void ExecuteCalculateShareBonusCommand(ProjectBonuses projectBonus)
        {
            if (!ValidatePercent(projectBonus)) return;
            MessageBox.Show(projectBonus.Percent.ToString());
            projectBonus.Amount = (decimal)(projectBonus.Percent * BonusSalary) / 100;
            LoadAmountInBonuses();
        }

        private bool ValidatePercent(ProjectBonuses projectBonus)
        {
            int newPercent = LoadNextPercentRemaining();
            if (newPercent < 0)
            {
                projectBonus.Percent = 0;
                return false;
            }
            percentRemaining = newPercent;
            return true;
        }

        private void ExecuteRestoreBonusCommand(ProjectBonuses projectBonus)
        {
            bonusNormalList.Add(projectBonus);
            bonusSpecial.Remove(projectBonus);
            percentRemaining = LoadNextPercentRemaining();
            LoadAmountInBonuses();
        }

        private void ExecuteRestoreNonBonusCommand(ProjectBonuses projectBonus)
        {

            bonusNormalList.Add(projectBonus);
            nonBonusList.Remove(projectBonus);
            LoadAmountInBonuses();
        }

        private void ExecuteAddShareBonusToDBCommand(object obj)
        {
            var dialog = new AlertDialogService(
                "Chia tiền thưởng",
                "Bạn chắc chắn muốn lưu các thao tác chia tiền thưởng!",
                () =>
                {
                    AddShareBonusToDBCommand(bonusNormalList);
                    AddShareBonusToDBCommand(bonusSpecial);
                }, null);
            dialog.Show();
        }

        private void AddShareBonusToDBCommand(ObservableCollection<ProjectBonuses> list)
        {
            if (list.Count == 0) return;
            foreach(ProjectBonuses projectBonuses in list)
            {
                projectBonuses.ID = AutoGenerateID();
                projectBonusesDao.Add(projectBonuses);
            }
        }

        public void ReceiveProjectID(string projectID)
        {
            this.projectID = projectID;
            BonusSalary = projectsDao.SearchByID(projectID).BonusSalary;
            SetProjectBonuese();
        }
    }
}
