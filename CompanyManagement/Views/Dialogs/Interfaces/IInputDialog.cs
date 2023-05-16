using CompanyManagement.ViewModels.Dialogs.Interfaces;

namespace CompanyManagement.Views.Dialogs.Interfaces
{
    public interface IInputDialog<TObject>
    {
        IInputViewModel<TObject> ViewModel { get; }
        void ShowInputDialog();
    }
}
