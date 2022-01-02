using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using InternationalVillage_Admin.Component;
using InternationalVillage_Admin.Model;
using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Utilities;
using InternationalVillage_Admin.Utilties;

namespace InternationalVillage_Admin.ViewModel
{
    class OverviewPageViewModel:BaseViewModel
    {

        public ICommand SelectedItem { get; set; }
        public ICommand LoadTaken { get; set; }
        public ICommand LoadAvailable { get; set; }
        public ICommand LoadIncident { get; set; }
        public ICommand LoadAll { get; set; }

        public ICommand CheckinDateChanged { get; set; }
        public ICommand CheckoutDateChanged { get; set; }
        public ICommand CheckoutDateSetUp { get; set; }
        public ICommand TypeOfApartmentChanged { get; set; }

        public ICommand RenderApartment { get; set; }

        private DateTime checkinDate = System.DateTime.Now;
        private DateTime checkoutDate = System.DateTime.Now;

        private string typeofApartment = "";

        bool isCheckinDateCorrect = false;
        bool isCheckoutDateCorrect = false;
        bool isTypeOfApartmentCorrect = false;

        List<Apartment> AvailableList = new List<Apartment>();
        List<Apartment> TakenList = new List<Apartment>();
        List<Apartment> IncidentList = new List<Apartment>();

        public OverviewPageViewModel()
        {
            CheckoutDateSetUp = new RelayCommand<CalendarDateRange>((p) => { return true; }, (p) =>
            {
                p.Start = System.DateTime.MinValue;
                p.End = (checkinDate.AddDays(-1));

            });

            CheckinDateChanged = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                checkinDate = Validate.Instance.DateChanged(p);
                isCheckinDateCorrect = true;

            });
            CheckoutDateChanged = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                checkoutDate = Validate.Instance.DateChanged(p);
                isCheckoutDateCorrect = true;
            });

            TypeOfApartmentChanged = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                typeofApartment = Validate.Instance.SelecttionChanged(p);
                isTypeOfApartmentCorrect = true;
            });

            RenderApartment = new RelayCommand<WrapPanel>((p) => { return true; }, async (p) =>
            {
                await Task.Delay(500);
                if (isTypeOfApartmentCorrect && isCheckinDateCorrect && isCheckoutDateCorrect)
                {
                    ChangeTypeApartment change = new ChangeTypeApartment();

                    AvailableList = ApartmentStore.Instance.GetAvailableList(change.ChangeTypeOfApartment(typeofApartment), checkinDate, checkoutDate);
                    TakenList = ApartmentStore.Instance.GetTakenList(change.ChangeTypeOfApartment(typeofApartment), checkinDate, checkoutDate);
                    IncidentList = ApartmentStore.Instance.GetIncidentList(change.ChangeTypeOfApartment(typeofApartment));

                    LoadAllApartment(p);
                    FrameworkElement parent = p.Parent as FrameworkElement;
                    if (parent.Parent is Grid g)
                    {
                        if (g.FindName("All") is ListBoxItem all)
                        {
                            all.IsSelected = true;
                        }
                    }
                }
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
                LoadAllApartment(p);
            });
        }

        void LoadAllApartment(WrapPanel p)
        {
            p.Children.Clear();
            RenderAvailableList(p);
            RenderTakenList(p);
            RenderIncidentList(p);
        }

        void RenderAvailableList(WrapPanel p)
        {
            foreach (Apartment a in AvailableList)
            {
                ApartmentUC apartmentUC = new ApartmentUC();
                apartmentUC.ContentOfApartment.Text = a.Id;
                apartmentUC.Toggle.Visibility = System.Windows.Visibility.Hidden;
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
                apartmentUC.Toggle.Visibility = System.Windows.Visibility.Hidden;
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
                apartmentUC.Toggle.Visibility = System.Windows.Visibility.Hidden;
                p.Children.Add(apartmentUC);
            }
        }
    }
}
