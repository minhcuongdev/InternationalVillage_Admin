using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

using InternationalVillage_Admin.Component;

namespace InternationalVillage_Admin.ViewModel
{
    class ApartmentWindowViewModel:BaseViewModel
    {
        public ICommand LoadApartment { get; set; }

        public ICommand SelectedItem { get; set; }
        public ICommand LoadTaken { get; set; }
        public ICommand LoadAvailable { get; set; }
        public ICommand LoadIncident { get; set; }

        public ApartmentWindowViewModel()
        {
            LoadApartment = new RelayCommand<WrapPanel>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < 30; i++)
                {
                    ApartmentUC apartmentUC = new ApartmentUC();
                    apartmentUC.ContentOfApartment.Text = i.ToString();
                    p.Children.Add(apartmentUC);
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
                for (int i = 0; i < 10; i++)
                {
                    ApartmentUC apartmentUC = new ApartmentUC();
                    apartmentUC.ContentOfApartment.Text = i.ToString();
                    apartmentUC.StatusBg.Background = System.Windows.Media.Brushes.Red;
                    apartmentUC.Status.Text = "Taken";
                    p.Children.Add(apartmentUC);
                }
            });

            LoadAvailable = new RelayCommand<WrapPanel>((p) => { return true; }, (p) =>
            {
                p.Children.Clear();
                for (int i = 0; i < 20; i++)
                {
                    ApartmentUC apartmentUC = new ApartmentUC();
                    apartmentUC.ContentOfApartment.Text = i.ToString();
                    p.Children.Add(apartmentUC);
                }
            });

            LoadIncident = new RelayCommand<WrapPanel>((p) => { return true; }, (p) =>
            {
                p.Children.Clear();
                for (int i = 0; i < 5; i++)
                {
                    ApartmentUC apartmentUC = new ApartmentUC();
                    apartmentUC.ContentOfApartment.Text = i.ToString();
                    apartmentUC.StatusBg.Background = System.Windows.Media.Brushes.Brown;
                    apartmentUC.Status.Text = "Incident";
                    p.Children.Add(apartmentUC);
                }
            });
        }
    }
}
