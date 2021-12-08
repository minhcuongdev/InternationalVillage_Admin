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
        public ICommand Next { get; set; }
        public ICommand EnableButton { get; set; }
        public ICommand IDNumberChanged { get; set; }
        public ICommand VisaChanged { get; set; }
        public ICommand NextPage { get; set; }

        private string fullname = "";
        public string Fullname { get => fullname; set => fullname = value; }

        

        private string idNumber = "";
        public string IdNumber { get => idNumber; set => idNumber = value; }
        

        private string visa = "";
        public string Visa { get => visa; set => visa = value; }
        public object NavigationService { get; private set; }

        

        public HandleRequestViewModel()
        {
            LoadTable = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                List<ApartmentRequest> detailBookings =ApartmentRequestStore.Instance.GetBookingList();
                p.ItemsSource = detailBookings;
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
                PaymentStore.Instance.FindBill(IdNumber, visa);

            });
            Next = new RelayCommand<Button>((p) => { return ((!idNumber.Equals("")) | (!visa.Equals(""))); }, (p) =>
            {
                PaymentStore.Instance.FindBill(IdNumber, visa);
               
            });
            NextPage = new RelayCommand<Page>((p) => { return ((!idNumber.Equals("")) | (!visa.Equals(""))); }, (p) =>
            {
                bool checkId = false;
                if (visa.Equals("")) checkId = PaymentStore.Instance.FindBill(idNumber, "error");
                else if (idNumber.Equals("")) checkId = PaymentStore.Instance.FindBill("error", visa);
                else checkId = PaymentStore.Instance.FindBill(idNumber, visa);
                if (checkId == false)
                {
                    MessageBox.Show("Id number or visa is not exist");
                }
                else
                {
                    p.NavigationService.Navigate(new Uri("Pages/PaymentDetail.xaml", UriKind.RelativeOrAbsolute));
                }

            });


        }
        
        
    }
}
