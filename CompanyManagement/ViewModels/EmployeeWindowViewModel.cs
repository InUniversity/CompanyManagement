using CompanyManagement.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    class EmployeeWindowViewModel : BaseViewModel
    {
        private ContentControl currentChildView = new AssignUC();

        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); } }

        public ICommand ShowAssignView { get; }


        public EmployeeWindowViewModel()
        {
            ShowAssignView = new RelayCommand<object>(ExecuteShowAssignView);          
        }

        private void ExecuteShowAssignView(object obj)
        {
            currentChildView.Content = new AssignUC();

        }
    }
}
