﻿using System;

namespace CompanyManagement.ViewModels
{
    public class ProjectInputViewModel : BaseViewModel, IRetrieveProject
    {
        private string id="";
        public string ID { get => id; set { id = value; OnPropertyChanged(); } }

        private string name = "";
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        private DateTime start = DateTime.Now;
        public DateTime Start { get => start; set { start = value; OnPropertyChanged(); } }

        private DateTime end = DateTime.Now;
        public DateTime End { get => end; set { end = value; OnPropertyChanged(); } }

        private string progress = "";
        public string Progress { get => progress; set { progress = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }
        
        public Project CreateProjectInstance()
        {
            return new Project(ID, Name,  Utils.DateToString(Start), Utils.DateToString(End),Progress);
        }

        public bool CheckAllFields()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(ID) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Progress))
            {
                ErrorMessage = "Các thông tin không được để trống!!!";
                return false;
            }                  
            if (End < start)
            {
                ErrorMessage = "Thời gian kết thúc không hợp lệ!!!";
                return false;
            }
            return true;
        }

        public void TrimAllTexts()
        {
            id = id.Trim();
            name = name.Trim();
            progress = progress.Trim(); 
        }

        public void Retrieve(Project project)
        {
            ID = project.ID;
            Name = project.Name;
            Progress = project.Progress;
            start = Utils.StringToDate(project.Start);
            end = Utils.StringToDate(project.End);
        }
    }
   
    public interface IRetrieveProject
    {
        void Retrieve(Project project);
    }
}
