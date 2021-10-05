using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using InternationalVillage_Admin.Store;

namespace InternationalVillage_Admin.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public ICommand CloseLoginWindow { get; set; }
        public ICommand ClearFocus { get; set; }
        public ICommand SignIn { get; set; }
        public ICommand HandleTextChanged { get; set; }
        public ICommand HandlePasswordChanged { get; set; }

        private string username = "";
        private string password = "";

        public string Username { get => username; set { username = value; OnPropertyChanged(); } }
        public string Password { get => password; set { password = value; OnPropertyChanged(); } }

        public LoginViewModel()
        {
            HandleTextChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                Username = p.Text;
            });
            HandlePasswordChanged = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                Password = p.Password;
            });
            CloseLoginWindow = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            ClearFocus = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Keyboard.ClearFocus();
            });

            SignIn = new RelayCommand<Window>((p) => {
                return !Username.Equals("") && !Password.Equals("");
            }, (p) =>
            {
                if (AccountStore.Instance.Authentication(Username, Password))
                {
                    MainWindow main = new MainWindow();
                    MainViewModel mv = main.DataContext as MainViewModel;
                    main.Show();
                    p.Close();
                }
                else
                {
                    MessageBox.Show("Wrong Account !! Please, Enter again !!");
                }
            });
        }
    }
}
