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

namespace InternationalVillage_Admin.ViewModel
{
    class BookApartmentServicePageViewModel:BaseViewModel
    {
        public ICommand LoadApartment { get; set; }
        public ICommand SelectionChanged { get; set; }
        public ICommand Render { get; set; }

        

        List<Apartment> LuxuryList = new List<Apartment>();
        List<Apartment> StandardList = new List<Apartment>();
        List<Apartment> NormalList = new List<Apartment>();

        string type= "";

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
    }
}
