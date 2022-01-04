using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Services;

namespace InternationalVillage_Admin.ViewModel
{
    class MenuViewModel:BaseViewModel
    {
        private IChatService chatService;

        public ICommand OpenHomePage { get; set; }
        public ICommand OpenActivityReceptionistPage { get; set; }
        public ICommand OpenChatPage { get; set; }

        public ICommand OpenProfilePage { get; set; }

        public ICommand Signout { get; set; }
        public ICommand MouseEnter { get; set; }
        public ICommand MouseLeave { get; set; }

        public ICommand LoadAvatar { get; set; }
        public ICommand LoadName { get; set; }

        public MenuViewModel(IChatService chatSvc)
        {
            chatService = chatSvc;
            ChatStore.Instance.ChatService = chatService;
            OpenHomePage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Navigate(new System.Uri("Pages/HomePage.xaml", UriKind.RelativeOrAbsolute));
            });

            OpenActivityReceptionistPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                switch(AccountStore.Instance.Role.ToString())
                {
                    case "Receptionist":
                        p.Navigate(new System.Uri("Pages/ReceptionistPage.xaml", UriKind.RelativeOrAbsolute));
                        break;
                    case "Accountant":
                        p.Navigate(new System.Uri("Pages/AccountantPage.xaml", UriKind.RelativeOrAbsolute));
                        break;
                    case "Manager":
                        p.Navigate(new System.Uri("Pages/DirectorPage.xaml", UriKind.RelativeOrAbsolute));
                        break;
                    default:
                        break;
                }
                
            });
            
            OpenChatPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Navigate(new System.Uri("Pages/ChatPage.xaml", UriKind.RelativeOrAbsolute));
            });

            OpenProfilePage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Navigate(new System.Uri("Pages/EditProfile.xaml", UriKind.RelativeOrAbsolute));
            });

            Signout = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                LoginWindow loginform = new LoginWindow();
                loginform.Show();
                p.Close();
            });

            MouseEnter = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                BrushConverter bc = new BrushConverter();
                p.Background = (Brush)bc.ConvertFrom("#F0F8FF");
                p.Foreground = (Brush)bc.ConvertFrom("#F0F8FF");
            });

            MouseLeave = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {

                p.Background = Brushes.Transparent;
            });

            LoadAvatar = new RelayCommand<ImageBrush>((p) => { return true; }, (p) =>
            {
                if (!AccountStore.Instance.Avatar.Equals(""))
                    p.ImageSource = new ImageSourceConverter().ConvertFromString(AccountStore.Instance.Avatar) as ImageSource;
            });

            LoadName = new RelayCommand<Label>((p) => { return true; }, (p) =>
            {
                p.Content = AccountStore.Instance.Name;
            });
        }

        #region Logout Command
        private ICommand _logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand =
                    new RelayCommandAsync(() => Logout(), (o) => CanLogout()));
            }
        }

        private async Task<bool> Logout()
        {
            try
            {
                await chatService.LogoutAsync();
                //UserMode = UserModes.Login;
                ChatStore.Instance.IsLoggedIn = false;
                ChatStore.Instance.IsConnected = false;
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        private bool CanLogout()
        {
            return ChatStore.Instance.IsConnected && ChatStore.Instance.IsLoggedIn;
        }
        #endregion        

        #region Connect Command
        private ICommand _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new RelayCommandAsync(() => Connect()));
            }
        }

        private async Task<bool> Connect()
        {
            try
            {
                await chatService.ConnectAsync();
                ChatStore.Instance.IsConnected = true;
                return true;
            }
            catch (Exception) { return false; }
        }
        #endregion
    }
}
