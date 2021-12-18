using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Model;
using InternationalVillage_Admin.Pages;
using System.Windows;
using System.Windows.Navigation;
using System.Diagnostics;

namespace InternationalVillage_Admin.ViewModel
{
    class HandleRequestViewModel : BaseViewModel
    {
        public ICommand LoadTable { get; set; }
        public ICommand SelectedTable { get; set; }
        public ICommand Next { get; set; }
        public ICommand EnableButton { get; set; }
        public ICommand InformationChanged { get; set; }
        public ICommand VisaChanged { get; set; }
        public ICommand NextPage { get; set; }
        public ICommand FindBill { get; set; }

        Bill selectedtale;
        private string fullname = "";
        public string Fullname { get => fullname; set => fullname = value; }

        

        private string information = "";
        public string Information { get => information; set => information = value; }
        

        private string visa = "";
        public string Visa { get => visa; set => visa = value; }
        public object NavigationService { get; private set; }

        

        public HandleRequestViewModel()
        {
            LoadTable = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                List<ApartmentRequest> detailBookings = ApartmentRequestStore.Instance.GetBookingList();
                p.ItemsSource = detailBookings;
            });

            SelectedTable = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                selectedtale = p.SelectedItem as Bill;

            });

            InformationChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                information = p.Text;

            });
            VisaChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                visa = p.Text;

            });

            EnableButton = new RelayCommand<Button>((p) => { return ((!information.Equals("")) | (!visa.Equals(""))); }, (p) =>
            {
               // PaymentStore.Instance.FindBill(information, visa);

            });
            Next = new RelayCommand<Button>((p) => { return ((!information.Equals("")) | (!visa.Equals(""))); }, (p) =>
            {
              //  PaymentStore.Instance.FindBill(information, visa);
               
            });
            NextPage = new RelayCommand<Page>((p) => { return (selectedtale != null); }, (p) =>
            {
                PaymentStore.Instance.FindDetailBill(selectedtale.Id);
                p.NavigationService.Navigate(new Uri("Pages/PaymentDetail.xaml", UriKind.RelativeOrAbsolute));
                
                
            });

            FindBill = new RelayCommand<DataGrid>((p) => { return ((!information.Equals(""))); }, (p) =>
            {
                
                    List<Bill> list = PaymentStore.Instance.GetListBill(information);
                    p.ItemsSource = list;
                    selectedtale = null;
                    if (list.Count == 0) MessageBox.Show("Information not found");
                

            });


        }
        
        
    }
}
