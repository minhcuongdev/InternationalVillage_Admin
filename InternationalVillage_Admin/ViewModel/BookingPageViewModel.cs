using InternationalVillage_Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Utilities;
using System.Windows;

namespace InternationalVillage_Admin.ViewModel
{
    class BookingPageViewModel : BaseViewModel
    {
        public ICommand ClearFocus { get; set; }
        public ICommand TextChange { get; set; }
        public ICommand FindCustomer { get; set; }
        public ICommand SelectItem { get; set; }

        public ICommand FillFullName { get; set; }
        public ICommand FullNameChanged { get; set; }
        public ICommand ValidateFullName { get; set; }

        public ICommand FillId { get; set; }
        public ICommand IDChanged { get; set; }
        public ICommand ValidateID { get; set; }

        public ICommand FillVisa { get; set; }
        public ICommand VisaChanged { get; set; }
        public ICommand ValidateVisa { get; set; }

        public ICommand CheckInDateChange { get; set; }
        public ICommand ValidateCheckInDate { get; set; }

        public ICommand CheckOutDateChange { get; set; }
        public ICommand ValidateCheckOutDate { get; set; }

        public ICommand CheckOutDateSetUp { get; set; }
        public ICommand CheckInDateSetUp { get; set; }

        public ICommand Next { get; set; }

        //Validate data
        private string fullname = "";
        public string Fullname { get => fullname; set => fullname = value; }
        bool isFullNameCorrect = false;

        private string id = "";
        public string Id { get => id; set => id = value; }
        bool isIdCorrect = false;

        private string visa = "";
        public string Visa { get => visa; set => visa = value; }
        bool isVisaCorrect = false;


        private DateTime checkinDate = System.DateTime.Now;
        public DateTime CheckinDate { get => checkinDate; set => checkinDate = value; }
        private string strCheckinDate = "";
        public string StrCheckinDate { get => strCheckinDate; set => strCheckinDate = value; }
        bool isCheckinDateCorrect = false;


        private DateTime checkoutDate = System.DateTime.Now;
        public DateTime CheckoutDate { get => checkoutDate; set => checkoutDate = value; }
        private string strCheckoutDate = "";
        public string StrCheckoutDate { get => strCheckoutDate; set => strCheckoutDate = value; }

        bool isCheckoutDateCorrect = false;



        public Customer cus = new Customer();

        string info = "";

        public BookingPageViewModel()
        {
            ClearFocus = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                Keyboard.ClearFocus();
            });
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
                fullname = p.Text;
                isFullNameCorrect = true;
            });

            FillId = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                p.Text = cus.Identification;
                Id = p.Text;
                if (Id.Equals("")) isIdCorrect = false;
                else isIdCorrect = true;
            });

            FillVisa = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                p.Text = cus.Visa;
                Visa = p.Text;
                if (Visa.Equals("")) isVisaCorrect = false;
                else isVisaCorrect = true;
                
            });

            CheckInDateChange = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                BookingStore.Instance.CheckIn = p.SelectedDate.Value;
                checkinDate = Validate.Instance.DateChanged(p);
                strCheckinDate = checkinDate.ToString();

            });
            ValidateCheckInDate = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                isCheckinDateCorrect = Validate.Instance.Required(p, strCheckinDate, "Check in Date");
            });

            ValidateCheckOutDate = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                isCheckoutDateCorrect = Validate.Instance.Required(p, strCheckoutDate, "Check out Date");

            });

            CheckOutDateChange = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                BookingStore.Instance.CheckOut = p.SelectedDate.Value;
                checkoutDate = Validate.Instance.DateChanged(p);
                strCheckoutDate = checkoutDate.ToString();
            });

            CheckInDateSetUp = new RelayCommand<CalendarDateRange>((p) => { return true; }, (p) =>
            {
                p.Start = System.DateTime.MinValue;
                p.End = (System.DateTime.Now.AddDays(-1));

            });
            CheckOutDateSetUp = new RelayCommand<CalendarDateRange>((p) => { return true; }, (p) =>
            {
                p.Start = System.DateTime.MinValue;
                p.End = (checkinDate.AddDays(-1));

            });

            FullNameChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                fullname = Validate.Instance.TextChanged(p, 5);

            });
            ValidateFullName = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                isFullNameCorrect = Validate.Instance.Required(p, fullname, "Full Name", 5);
            });

            IDChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                id = Validate.Instance.TextChanged(p);

            });
            ValidateID = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                if (Visa.Equals("") || !Id.Equals(""))
                {
                    isIdCorrect = Validate.Instance.Required(p, Id, "Identification");
                    if (isIdCorrect)
                    {
                        isIdCorrect = Validate.Instance.Number(p, Id, "Identification", 9);
                    }
                }
                else
                {
                    isIdCorrect = true;
                    p.Visibility = Visibility.Hidden;
                }
            });

            VisaChanged = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                Visa = Validate.Instance.TextChanged(p);
            });
            ValidateVisa = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                if (Id.Equals("") || !Visa.Equals(""))
                {
                    isVisaCorrect = Validate.Instance.Required(p, Visa, "VISA", 7);
                }
                else
                {
                    isVisaCorrect = true;
                    p.Visibility = Visibility.Hidden;
                }
            });



            Next = new RelayCommand<Page>((p) => { return isFullNameCorrect  && isCheckinDateCorrect && isCheckoutDateCorrect
                                                            && (isIdCorrect || isVisaCorrect);
            }, (p) =>
            {
                BookingStore.Instance.IdCustomer = cus.IdCustomer;
                p.NavigationService.Navigate(new Uri("Pages/BookApartmentService.xaml", UriKind.RelativeOrAbsolute));
            });
        }

    }
}
