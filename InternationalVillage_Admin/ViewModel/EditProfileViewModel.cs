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
    class EditProfileViewModel : BaseViewModel
    {
        public ICommand OpenChangePassword { get; set; }

        public EditProfileViewModel()
        {
            OpenChangePassword = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                ChangePasswordWindow changepassform = new ChangePasswordWindow();
                changepassform.ShowDialog();
            });
        }
    }
}
