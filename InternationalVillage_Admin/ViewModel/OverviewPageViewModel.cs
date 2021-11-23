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
    class OverviewPageViewModel:BaseViewModel
    {
        public ICommand LoadApartment { get; set; }

        public OverviewPageViewModel()
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
        }
    }
}
