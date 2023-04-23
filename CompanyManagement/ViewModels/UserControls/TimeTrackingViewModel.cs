using System.Collections.Generic;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Services;

namespace CompanyManagement.ViewModels.UserControls
{
    public class TimeTrackingViewModel : BaseViewModel, IRetrieveProjectID
    {

        
        public TimeTrackingViewModel()
        {
            
        }

        public void RetrieveProjectID(string projectID)
        {
            throw new System.NotImplementedException();
        }
    }
}