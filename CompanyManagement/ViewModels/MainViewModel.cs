using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand LoadedWindowCommand { get; set; }

        public MainViewModel()
        {
            SetCommand();
        }

        public void SetCommand()
        {
            LoadedWindowCommand = new RelayCommand<Window>(p => {
                IsLoaded = true;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;

                var loginWM = loginWindow.DataContext as LoginViewModel;

                if (loginWM.IsLogin == true)
                {
                    p.Show();
                }
                else
                {
                    p.Close();
                }    
            });
        }
    }
}
