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

namespace InternationalVillage_Admin.ViewModel
{
    class MenuViewModel:BaseViewModel
    {
        public ICommand OpenHomePage { get; set; }
        public ICommand OpenActivityReceptionistPage { get; set; }
        public ICommand OpenChatPage { get; set; }

        public ICommand OpenProfilePage { get; set; }

        public ICommand Signout { get; set; }
        public ICommand MouseEnter { get; set; }
        public ICommand MouseLeave { get; set; }



        public MenuViewModel()
        {
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
        }
    }
}
