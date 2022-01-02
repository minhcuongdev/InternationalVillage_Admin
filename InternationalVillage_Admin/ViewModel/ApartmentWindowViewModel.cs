using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

using InternationalVillage_Admin.Component;
using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Model;
using InternationalVillage_Admin.Utilties;
using System.Windows;

namespace InternationalVillage_Admin.ViewModel
{
    class ApartmentWindowViewModel:BaseViewModel
    {
        public ICommand LoadApartment { get; set; }

        public ICommand SelectedItem { get; set; }
        public ICommand LoadTaken { get; set; }
        public ICommand LoadAvailable { get; set; }
        public ICommand LoadIncident { get; set; }
        public ICommand LoadAll { get; set; }

        public ICommand LoadNumber { get; set; }
        public ICommand HandleRequest { get; set; }

        List<Apartment> AvailableList = new List<Apartment>();
        List<Apartment> TakenList = new List<Apartment>();
        List<Apartment> IncidentList = new List<Apartment>();

        public ApartmentWindowViewModel()
        {
            LoadApartment = new RelayCommand<WrapPanel>((p) => { return true; }, (p) =>
            {

                ChangeTypeApartment change = new ChangeTypeApartment();

                AvailableList = ApartmentStore.Instance.GetAvailableList(change.ChangeTypeOfApartment(ApartmentRequestStore.Instance.ApartmentRequest.Type), ApartmentRequestStore.Instance.ApartmentRequest.CheckIn, ApartmentRequestStore.Instance.ApartmentRequest.CheckOut);
                RenderAvailableList(p);

                TakenList = ApartmentStore.Instance.GetTakenList(change.ChangeTypeOfApartment(ApartmentRequestStore.Instance.ApartmentRequest.Type), ApartmentRequestStore.Instance.ApartmentRequest.CheckIn, ApartmentRequestStore.Instance.ApartmentRequest.CheckOut);
                RenderTakenList(p);

                IncidentList = ApartmentStore.Instance.GetIncidentList(change.ChangeTypeOfApartment(ApartmentRequestStore.Instance.ApartmentRequest.Type));
                RenderIncidentList(p);
            });

            SelectedItem = new RelayCommand<ListBoxItem>((p) => { return true; }, (p) =>
            {
                if (p.Parent is ListBox list)
                {
                    list.SelectedItems.Clear();
                }

                p.IsSelected = true;

            });

            LoadTaken = new RelayCommand<WrapPanel>((p) => { return true; }, (p) =>
            {
                p.Children.Clear();
                RenderTakenList(p);
            });

            LoadAvailable = new RelayCommand<WrapPanel>((p) => { return true; }, (p) =>
            {
                p.Children.Clear();
                RenderAvailableList(p);
            });

            LoadIncident = new RelayCommand<WrapPanel>((p) => { return true; }, (p) =>
            {
                p.Children.Clear();
                RenderIncidentList(p);
            });

            LoadAll = new RelayCommand<WrapPanel>((p) => { return true; }, (p) =>
            {
                p.Children.Clear();
                RenderAvailableList(p);
                RenderTakenList(p);
                RenderIncidentList(p);
            });

            LoadNumber = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = ApartmentRequestStore.Instance.ApartmentRequest.Quantity.ToString();
            });

            HandleRequest = new RelayCommand<Window>((p) => { 
                if (ApartmentStore.Instance.ApartmentSlectedList.Count == 0 || 
                ApartmentStore.Instance.ApartmentSlectedList.Count != ApartmentRequestStore.Instance.ApartmentRequest.Quantity) 
                { 
                    return false; 
                } else 
                    return true; 
            }, (p) =>
            {

                if (PaymentStore.Instance.CreateBill(ApartmentRequestStore.Instance.ApartmentRequest.IdCustomer, ApartmentRequestStore.Instance.ApartmentRequest.CheckIn, ApartmentRequestStore.Instance.ApartmentRequest.CheckOut))
                {
                    if (NotificationStore.Instance.NotificationAcceptedRequisition(ApartmentRequestStore.Instance.ApartmentRequest.IdCustomer, DateTime.Now, "Reservation request has been accepted"))
                    {
                        string idBill = PaymentStore.Instance.IdBill;
                        ChangeTypeApartment change = new ChangeTypeApartment();
                        foreach (ApartmentUC uc in ApartmentStore.Instance.ApartmentSlectedList)
                        {
                            PaymentStore.Instance.InsertDetailApartmentBill(uc.ContentOfApartment.Text, idBill, change.ChangeTypeToPrice(ApartmentRequestStore.Instance.ApartmentRequest.Type), ApartmentRequestStore.Instance.ApartmentRequest.CheckIn, ApartmentRequestStore.Instance.ApartmentRequest.CheckOut);
                            ApartmentStore.Instance.InsertBookingApartmentTable(ApartmentRequestStore.Instance.ApartmentRequest.IdCustomer, uc.ContentOfApartment.Text, change.ChangeTypeToPrice(ApartmentRequestStore.Instance.ApartmentRequest.Type), ApartmentRequestStore.Instance.ApartmentRequest.CheckIn, ApartmentRequestStore.Instance.ApartmentRequest.CheckOut);
                        }
                        ApartmentRequestStore.Instance.UpdateState(ApartmentRequestStore.Instance.ApartmentRequest.CheckIn, ApartmentRequestStore.Instance.ApartmentRequest.CheckOut);
                        PaymentStore.Instance.UpdateToTal(idBill);
                    }
                }

                ApartmentStore.Instance.ApartmentSlectedList.Clear();
                PaymentStore.Instance.isFinished = true;
                p.Close();
            });
        }

        void RenderAvailableList(WrapPanel p)
        {
            foreach (Apartment a in AvailableList)
            {
                ApartmentUC apartmentUC = new ApartmentUC();
                apartmentUC.ContentOfApartment.Text = a.Id;
                p.Children.Add(apartmentUC);
            }
        }
        void RenderTakenList(WrapPanel p)
        {
            foreach (Apartment a in TakenList)
            {
                ApartmentUC apartmentUC = new ApartmentUC();
                apartmentUC.ContentOfApartment.Text = a.Id;
                apartmentUC.StatusBg.Background = System.Windows.Media.Brushes.Red;
                apartmentUC.Status.Text = "Taken";
                apartmentUC.Toggle.IsEnabled = false;
                p.Children.Add(apartmentUC);
            }
        }

        void RenderIncidentList(WrapPanel p)
        {
            foreach (Apartment a in IncidentList)
            {
                ApartmentUC apartmentUC = new ApartmentUC();
                apartmentUC.ContentOfApartment.Text = a.Id;
                apartmentUC.StatusBg.Background = System.Windows.Media.Brushes.Brown;
                apartmentUC.Status.Text = "Taken";
                apartmentUC.Toggle.IsEnabled = false;
                p.Children.Add(apartmentUC);
            }
        }

    }
}
