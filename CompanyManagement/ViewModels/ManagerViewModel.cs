using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class ManagerViewModel: BaseViewModel
    {
        public bool IsLoaded = false;

        public ICommand LoadedWindowCommand { get; set; }

        public ManagerViewModel()
        {
            SetCommands();
        }

        public void SetCommands()
        {
            LoadedWindowCommand = new RelayCommand<Window>(p => {
                IsLoaded = true;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;

                var loginWM = loginWindow.DataContext as LoginViewModel;

                if (loginWM.IsLogin == 1)
                {
                    p.Show();
                }
                else if(loginWM.IsLogin == 2)
                {
                    EmployeeWindow q = new EmployeeWindow();
                    q.Show();
                    p.Close();
                }    
                else
                {
                    p.Close();
                }    
            });
        }
    }
}
