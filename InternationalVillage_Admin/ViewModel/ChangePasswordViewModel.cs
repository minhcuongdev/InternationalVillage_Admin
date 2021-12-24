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
    class ChangePasswordViewModel : BaseViewModel
    {
        public ICommand CloseForm { get; set; }

        public ChangePasswordViewModel()
        {
            CloseForm = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
        }
    }
}
