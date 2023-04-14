using System;

namespace CompanyManagement.ViewModels.Dialogs.Interfaces
{
    public interface IInputViewModel<TObject>
    {
        void RetrieveObject(TObject obj);
        void RetrieveSubmitAction(Action<TObject> submitObjectAction);
    }
}
