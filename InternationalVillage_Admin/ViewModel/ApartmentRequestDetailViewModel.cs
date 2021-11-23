using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace InternationalVillage_Admin.ViewModel
{
    class ApartmentRequestDetailViewModel:BaseViewModel
    {
        public ICommand ShowApartmentForm { get; set; }

        public ApartmentRequestDetailViewModel()
        {
            ShowApartmentForm = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                ApartmentWindow chooseapartmentform = new ApartmentWindow();
                chooseapartmentform.Show();
                
            });
        }
    }
}
