using InternationalVillage_Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

using InternationalVillage_Admin.Store;

namespace InternationalVillage_Admin.ViewModel
{
    class BookingPageViewModel : BaseViewModel
    {
        public ICommand TextChange { get; set; }
        public ICommand FindCustomer { get; set; }
        public ICommand SelectItem { get; set; }

        public ICommand FillFullName { get; set; }
        public ICommand FillId { get; set; }
        public ICommand FillVisa { get; set; }

        public ICommand CheckInDateChange { get; set; }
        public ICommand CheckOutDateChange { get; set; }

        public ICommand Next { get; set; }

        public Customer cus = new Customer();

        string info = "";

        public BookingPageViewModel()
        {
            TextChange = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                info = p.Text;
            });

            FindCustomer = new RelayCommand<DataGrid>((p) => { if(info.Trim().Equals("")) return false; return true; }, (p) =>
            {
                List<Customer> list = BookingStore.Instance.GetCustomerList(info);
                p.ItemsSource = list;
            });

            SelectItem = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                cus = p.SelectedItem as Customer;
            });

            FillFullName = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                p.Text = cus.FullName;
            });

            FillId = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                p.Text = cus.Identification;
            });

            FillVisa = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                p.Text = cus.Visa;
            });

            CheckInDateChange = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                BookingStore.Instance.CheckIn = p.SelectedDate.Value;
                
            });

            CheckOutDateChange = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                BookingStore.Instance.CheckOut = p.SelectedDate.Value;
            });

            Next = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                BookingStore.Instance.IdCustomer = cus.IdCustomer;    
            });
        }

    }
}
