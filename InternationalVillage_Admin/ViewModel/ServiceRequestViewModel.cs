using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using InternationalVillage_Admin.Model;
using InternationalVillage_Admin.Store;

namespace InternationalVillage_Admin.ViewModel
{
    class ServiceRequestViewModel : BaseViewModel
    {
        public ICommand LoadTable { get; set; }
        public ICommand Approved { get; set; }
        public ICommand SelectedItem { get; set; }

        private ServiceRequest serviceSelected = null;

        public ServiceRequestViewModel()
        {
            LoadTable = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                serviceSelected = null;
                LoadServiceTable(p);
            });

            SelectedItem = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                serviceSelected = p.SelectedItem as ServiceRequest;
            });

            Approved = new RelayCommand<DataGrid>((p) => { if (serviceSelected != null) return true; return false; }, (p) =>
            {
                string idBill = PaymentStore.Instance.GetIdBill(serviceSelected.IdApartment, serviceSelected.CheckIn, serviceSelected.CheckOut);
                int price = serviceSelected.Quantity * serviceSelected.NumberPeople * serviceSelected.UnitPrice;
                if(PaymentStore.Instance.InsertDetailServiceBill(serviceSelected, idBill,price))
                {
                    ServiceRequestStore.Instance.UpdateState(serviceSelected);
                    PaymentStore.Instance.UpdateToTalService(idBill);
                    LoadServiceTable(p);
                }
                else
                {
                    MessageBox.Show("error");
                }

            });
        }

        void LoadServiceTable(DataGrid p)
        {
            List<ServiceRequest> serviceList = ServiceRequestStore.Instance.GetServiceRequestList();
            p.ItemsSource = serviceList;
        }

    }
}
