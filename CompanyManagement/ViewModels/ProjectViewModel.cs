using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {

        private ICommand addProject;

        public ICommand AddProject
        {
            get => addProject;
            set
            {
                addProject = value;
                OnPropertyChanged(nameof(addProject));
            } 
        }

        public void Add()
        {
            
        }

        public void Delete()
        {

        }

        public void Update() 
        {

        }
    }
}
