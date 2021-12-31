using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using InternationalVillage_Admin.Store;

namespace InternationalVillage_Admin.ViewModel
{
    class ApartmentRequestDetailViewModel:BaseViewModel
    {
        public ICommand ShowApartmentForm { get; set; }
        public ICommand LoadCustomer { get; set; }
        public ICommand LoadID { get; set; }
        public ICommand LoadBookingTime { get; set; }
        public ICommand LoadCheckInDate { get; set; }
        public ICommand LoadCheckOutDate { get; set; }
        public ICommand LoadTypeOfApartment { get; set; }
        public ICommand LoadQuantity { get; set; }
        public ICommand LoadStatus { get; set; }
        public ICommand Reject { get; set; }


        public ApartmentRequestDetailViewModel()
        {
            ShowApartmentForm = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                ApartmentWindow chooseapartmentform = new ApartmentWindow();
                chooseapartmentform.ShowDialog();
                if(PaymentStore.Instance.isFinished)
                {
                    p.NavigationService.Navigate(new Uri("Pages/HandleRequest.xaml", UriKind.RelativeOrAbsolute));
                    PaymentStore.Instance.isFinished = false;
                }
            });

            Reject = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                ApartmentRequestStore.Instance.UpdateState(ApartmentRequestStore.Instance.ApartmentRequest.CheckIn, ApartmentRequestStore.Instance.ApartmentRequest.CheckOut,"Reject");
                NotificationStore.Instance.NotificationAcceptedRequisition(ApartmentRequestStore.Instance.ApartmentRequest.IdCustomer, DateTime.Now, "Reservation request has been declined");
                p.NavigationService.Navigate(new Uri("Pages/HandleRequest.xaml", UriKind.RelativeOrAbsolute));
                ApartmentStore.Instance.ApartmentSlectedList.Clear();
                PaymentStore.Instance.isFinished = false;
            });

            LoadCustomer = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = ApartmentRequestStore.Instance.ApartmentRequest.Fullname;
            });

            LoadID = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = ApartmentRequestStore.Instance.ApartmentRequest.ID.ToString();
            });

            LoadBookingTime = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = ApartmentRequestStore.Instance.ApartmentRequest.BookingTime;
            });

            LoadCheckInDate = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = ApartmentRequestStore.Instance.ApartmentRequest.CheckIn.ToString();
            });

            LoadCheckOutDate = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = ApartmentRequestStore.Instance.ApartmentRequest.CheckOut.ToString();
            });

            LoadTypeOfApartment = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = ApartmentRequestStore.Instance.ApartmentRequest.Type;
            });

            LoadQuantity = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = ApartmentRequestStore.Instance.ApartmentRequest.Quantity.ToString();
            });

            LoadStatus = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = ApartmentRequestStore.Instance.ApartmentRequest.State;
            });
        }
    }
}
