﻿using System.Collections.Generic;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.ProjectsView
{
    public interface IProjectsStrategy
    {
        void SetVisible(IProjects viewModel);
        List<Project> GetProjects(string employeeID);
    }
}