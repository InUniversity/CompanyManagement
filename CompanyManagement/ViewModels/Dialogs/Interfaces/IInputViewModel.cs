using System;

namespace CompanyManagement.ViewModels.Dialogs.Interfaces
{
    public interface IInputViewModel<TObject>
    {
        void ReceiveObject(TObject request);
        void ReceiveSubmitAction(Action<TObject> submitObjectAction);
    }
}
