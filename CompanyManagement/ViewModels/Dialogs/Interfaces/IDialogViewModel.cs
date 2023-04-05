using CompanyManagement.ViewModels.UserControls.Interfaces;

namespace CompanyManagement.ViewModels.Dialogs.Interfaces
{
    public interface IDialogViewModel
    {
        IEditDBViewModel ParentDataContext { set; }
        void Retrieve(object obj);
    }
}
