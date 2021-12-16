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
        public ICommand IDNumberChanged { get; set; }
        public ICommand VisaChanged { get; set; }
        public ICommand NextPage { get; set; }
        public ICommand FindBill { get; set; }

        Bill selectedtale;
        private string fullname = "";
        public string Fullname { get => fullname; set => fullname = value; }

        

        private string idNumber = "";
        public string IdNumber { get => idNumber; set => idNumber = value; }
        

        private string visa = "";
        public string Visa { get => visa; set => visa = value; }
        public object NavigationService { get; private set; }

        

        public HandleRequestViewModel()
        {
           // LoadTable = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
           // {
           //    List<ApartmentRequest> detailBookings =ApartmentRequestStore.Instance.GetBookingList();
           //     p.ItemsSource = detailBookings;
           // });

            SelectedTable = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                selectedtale = p.SelectedItem as Bill;

            });

            IDNumberChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                idNumber = p.Text;

            });
            VisaChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                visa = p.Text;

            });

            EnableButton = new RelayCommand<Button>((p) => { return ((!idNumber.Equals("")) | (!visa.Equals(""))); }, (p) =>
            {
               // PaymentStore.Instance.FindBill(IdNumber, visa);

            });
            Next = new RelayCommand<Button>((p) => { return ((!idNumber.Equals("")) | (!visa.Equals(""))); }, (p) =>
            {
              //  PaymentStore.Instance.FindBill(IdNumber, visa);
               
            });
            NextPage = new RelayCommand<Page>((p) => { return (selectedtale != null); }, (p) =>
            {
                PaymentStore.Instance.FindDetailBill(selectedtale.Id);
                p.NavigationService.Navigate(new Uri("Pages/PaymentDetail.xaml", UriKind.RelativeOrAbsolute));
                
                
            });

            FindBill = new RelayCommand<DataGrid>((p) => { return ((!idNumber.Equals("")) | (!visa.Equals(""))); }, (p) =>
            {
                bool checkId = false;
                if (visa.Equals("")) checkId = PaymentStore.Instance.CheckID(idNumber,"error");
                else if (idNumber.Equals("")) checkId = PaymentStore.Instance.CheckID("error",visa);
                else checkId = PaymentStore.Instance.CheckID(idNumber, visa);
                if (checkId)
                {
                    List<Bill> list = PaymentStore.Instance.GetListBill(idNumber, visa);
                    p.ItemsSource = list;
                    selectedtale = null;
                }
                else
                {
                    MessageBox.Show("Id number or visa is not exist");
                }

            });


        }
        
        
    }
}
