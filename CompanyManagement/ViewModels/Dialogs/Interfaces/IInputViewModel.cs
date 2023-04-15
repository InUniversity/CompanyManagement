using System;

namespace CompanyManagement.ViewModels.Dialogs.Interfaces
{
    public interface IInputViewModel<TObject>
    {
        void ReceiveObject(TObject obj);
        void ReceiveSubmitAction(Action<TObject> submitObjectAction);
    }
}
