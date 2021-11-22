using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InternationalVillage_Admin.ViewModel
{
    class MenuViewModel:BaseViewModel
    {
        public ICommand OpenHomePage { get; set; }
        public ICommand OpenActivityReceptionistPage { get; set; }
        public ICommand OpenSettingPage { get; set; }

        public ICommand Signout { get; set; }



        public MenuViewModel()
        {
            OpenHomePage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Navigate(new System.Uri("Pages/HomePage.xaml", UriKind.RelativeOrAbsolute));
            });

            OpenActivityReceptionistPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Navigate(new System.Uri("Pages/ReceptionistPage.xaml", UriKind.RelativeOrAbsolute));
            });
            
            OpenSettingPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Navigate(new System.Uri("Pages/SettingPage.xaml", UriKind.RelativeOrAbsolute));
            });

            Signout = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                LoginWindow loginform = new LoginWindow();
                loginform.Show();
                p.Close();


            });
        }
    }
}
