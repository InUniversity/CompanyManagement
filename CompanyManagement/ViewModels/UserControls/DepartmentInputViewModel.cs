using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.UserControls
{

    public interface IDepartmentInput
    {
        string ID { get; }
        string Name { get; }
        string ErrorMessage { set; }
        Department CreateDepartmentInstance();
        void TrimAllTexts();
        void Retrieve(Department department);
    }
    

    public class DepartmentInputViewModel: BaseViewModel, IDepartmentInput
    {

        private string id = "";
        public string ID { get => id; set { id = value; OnPropertyChanged(); } }

        private string name = "";
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        private string managerID = "";
        public string ManagerID { get => managerID; set { managerID = value; OnPropertyChanged(); } }

        private string errorMesange = "";
        public string ErrorMessage { get => errorMesange; set { errorMesange = value; OnPropertyChanged(); } }

        public DepartmentInputViewModel()
        {

        }

        public Department CreateDepartmentInstance()
        {
            return new Department(id, name, managerID);
        }

        public void TrimAllTexts()
        {
            id = id.Trim();
            name = name.Trim();
            managerID = managerID.Trim();
        }

        public void Retrieve(Department department)
        {
            ID = department.ID;
            Name = department.Name;
            managerID = department.ManagerID;
        }
    }
}
