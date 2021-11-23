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
    class PaymentDetailViewModel : BaseViewModel
    {
        public ICommand ShowBank { get; set; }

        public PaymentDetailViewModel()
        {
            ShowBank = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                
                p.Visibility=Visibility.Visible;
            });
        }
    }
}
