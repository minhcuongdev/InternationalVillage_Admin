using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Model;
using System.Windows;

namespace InternationalVillage_Admin.ViewModel
{
    class HandleRequestViewModel : BaseViewModel
    {
        public ICommand LoadTable { get; set; }

        public HandleRequestViewModel()
        {
            LoadTable = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                List<ApartmentRequest> detailBookings =ApartmentRequestStore.Instance.GetBookingList();
                p.ItemsSource = detailBookings;
            });

        }
    }
}
