using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

using InternationalVillage_Admin.Component;
using InternationalVillage_Admin.Model;
using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Utilties;
using System.Windows.Media;

namespace InternationalVillage_Admin.ViewModel
{
    class BookApartmentServicePageViewModel:BaseViewModel
    {
        public ICommand LoadApartment { get; set; }
        public ICommand SelectionChanged { get; set; }
        public ICommand Render { get; set; }

        public ICommand Submit { get; set; }

        public ICommand UsageTime_Pool { get; set; }
        public ICommand People_Pool { get; set; }

        public ICommand UsageTime_Restaurant { get; set; }
        public ICommand People_Restaurant { get; set; }

        public ICommand UsageTime_Gym { get; set; }
        public ICommand People_Gym { get; set; }

        public ICommand UsageTime_Tennis { get; set; }
        public ICommand People_Tennis { get; set; }

        public ICommand UsageTime_Golf { get; set; }
        public ICommand People_Golf { get; set; }

        public ICommand UsageTime_Bar { get; set; }
        public ICommand People_Bar { get; set; }


        List<Apartment> LuxuryList = new List<Apartment>();
        List<Apartment> StandardList = new List<Apartment>();
        List<Apartment> NormalList = new List<Apartment>();

        string type= "";

        List<int> Pool = new List<int>() { 0, 0 };
        List<int> Restaurant = new List<int>() { 0,0 };
        List<int> Gym = new List<int>() { 0, 0 };
        List<int> Tennis = new List<int>() { 0, 0 };
        List<int> Golf = new List<int>() { 0, 0 };
        List<int> Bar = new List<int>() { 0, 0 };

        public BookApartmentServicePageViewModel()
        {
            LoadApartment = new RelayCommand<WrapPanel>((p) => { return true; }, (p) =>
            {
                LuxuryList = ApartmentStore.Instance.GetAvailableList("3A",BookingStore.Instance.CheckIn);
                StandardList = ApartmentStore.Instance.GetAvailableList("3B",BookingStore.Instance.CheckIn);
                NormalList = ApartmentStore.Instance.GetAvailableList("2A",BookingStore.Instance.CheckIn);
                RenderAvailableList(p,LuxuryList);
            });

            SelectionChanged = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                ComboBoxItem typeItem = (ComboBoxItem)p.SelectedItem;
                type = typeItem.Content.ToString();
            });

            UsageTime_Pool = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                TextChange(p, Pool, 0);
            });

            People_Pool = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                TextChange(p, Pool, 1);
            });


            UsageTime_Restaurant = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                TextChange(p, Restaurant, 0);
            });

            People_Restaurant = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                TextChange(p, Restaurant, 1);
            });

            UsageTime_Gym = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                TextChange(p, Gym, 0);
            });

            People_Gym = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                TextChange(p, Gym, 1);
            });

            UsageTime_Tennis = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                TextChange(p, Tennis, 0);
            });

            People_Tennis = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                TextChange(p, Tennis, 1);
            });

            UsageTime_Golf = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                TextChange(p, Golf, 0);
            });

            People_Golf = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                TextChange(p, Golf, 1);
            });

            UsageTime_Bar = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                TextChange(p, Bar, 0);
            });

            People_Bar = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                TextChange(p, Bar, 1);
            });

            Render = new RelayCommand<WrapPanel>((p) => { return true; }, (p) =>
            {
                switch(type)
                {
                    case "Luxury":
                        RenderAvailableList(p, LuxuryList);
                        break;
                    case "High standard":
                        RenderAvailableList(p, StandardList);
                        break;
                    case "Normal":
                        RenderAvailableList(p, NormalList);
                        break;
                    default:
                        break;
                }
            });

            Submit = new RelayCommand<Page>((p) => 
            {
                if (ApartmentStore.Instance.ApartmentSlectedList.Count > 0)
                {
                    return true; 
                }
                return false;
            }, (p) =>
            {
                if (PaymentStore.Instance.CreateBill(BookingStore.Instance.IdCustomer, BookingStore.Instance.CheckIn, BookingStore.Instance.CheckOut))
                {
                    string idBill = PaymentStore.Instance.IdBill;
                    ChangeTypeApartment change = new ChangeTypeApartment();
                    foreach (ApartmentUC uc in ApartmentStore.Instance.ApartmentSlectedList)
                    {
                        PaymentStore.Instance.InsertDetailApartmentBill(uc.ContentOfApartment.Text, idBill, change.ChangeTypeToPrice(change.ChangeIdApartmentToType(uc.ContentOfApartment.Text)), BookingStore.Instance.CheckIn, BookingStore.Instance.CheckOut);
                        ApartmentStore.Instance.InsertBookingApartmentTable(BookingStore.Instance.IdCustomer, uc.ContentOfApartment.Text, change.ChangeTypeToPrice(change.ChangeIdApartmentToType(uc.ContentOfApartment.Text)), BookingStore.Instance.CheckIn, BookingStore.Instance.CheckOut);
                    }

                    InsertService(idBill, BookingStore.Instance.CheckIn, BookingStore.Instance.CheckOut);

                    PaymentStore.Instance.UpdateToTal(idBill);
                    PaymentStore.Instance.UpdateToTalService(idBill);
                }

                ApartmentStore.Instance.ApartmentSlectedList.Clear();
                MessageBox.Show("success");
                p.NavigationService.Navigate(new Uri("Pages/BookingPage.xaml", UriKind.RelativeOrAbsolute));
            });

        }

        void RenderAvailableList(WrapPanel p,List<Apartment> list)
        {
            p.Children.Clear();
            foreach (Apartment a in list)
            {
                ApartmentUC apartmentUC = new ApartmentUC();
                apartmentUC.ContentOfApartment.Text = a.Id;
                p.Children.Add(apartmentUC);
            }
        }

        public bool IsNumber(string pText)
        {
            Regex regex = null;
            try
            {
                regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$"); return regex.IsMatch(pText);
            }
            catch
            {
                return regex.IsMatch(pText);
            }
        }

        void TextChange(TextBox p,List<int> service,int index)
        {
            if (IsNumber(p.Text))
            {
                if (int.Parse(p.Text) != 0)
                {
                    service[index] = int.Parse(p.Text);
                    var bc = new BrushConverter();
                    p.BorderBrush = (Brush)bc.ConvertFrom("#89000000");
                }
                else
                    service[index] = 0;
            }
            else
            {
                if (p.Text.Trim().Equals(""))
                    service[index] = 0;
                p.BorderBrush = Brushes.Red;
            }
                
        }

        void InsertService(string idBill,DateTime Checkin, DateTime Checkout)
        {
            if (!(Pool[0] == 0 || Pool[1] == 0))
            {
                ServiceRequest s = new ServiceRequest("S01",Pool[0],Checkin,Checkout);
                int price = Pool[0] * Pool[1] * 0;
                PaymentStore.Instance.InsertDetailServiceBill(s, idBill, price);
            }

            if (!(Restaurant[0] == 0 || Restaurant[1] == 0))
            {
                ServiceRequest s = new ServiceRequest("S03",Restaurant[0],Checkin,Checkout);
                int price = Pool[0] * Pool[1] * 20;
                PaymentStore.Instance.InsertDetailServiceBill(s, idBill, price);
            }

            if (!(Gym[0] == 0 || Gym[1] == 0))
            {
                ServiceRequest s = new ServiceRequest("S02", Gym[0], Checkin, Checkout);
                int price = Pool[0] * Pool[1] * 9;
                PaymentStore.Instance.InsertDetailServiceBill(s, idBill, price);
            }

            if (!(Tennis[0] == 0 || Tennis[1] == 0))
            {
                ServiceRequest s = new ServiceRequest("S04", Tennis[0], Checkin, Checkout);
                int price = Pool[0] * Pool[1] * 13;
                PaymentStore.Instance.InsertDetailServiceBill(s, idBill, price);
            }

            if (!(Golf[0] == 0 || Golf[1] == 0))
            {
                ServiceRequest s = new ServiceRequest("S05", Golf[0], Checkin, Checkout);
                int price = Pool[0] * Pool[1] * 22;
                PaymentStore.Instance.InsertDetailServiceBill(s, idBill, price);
            }

            if (!(Bar[0] == 0 || Bar[1] == 0))
            {
                ServiceRequest s = new ServiceRequest("S06", Bar[0], Checkin, Checkout);
                int price = Pool[0] * Pool[1] * 22;
                PaymentStore.Instance.InsertDetailServiceBill(s, idBill, price);
            }
        }
    }
}
