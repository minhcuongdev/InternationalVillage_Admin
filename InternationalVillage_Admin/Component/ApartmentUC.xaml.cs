using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using InternationalVillage_Admin.ViewModel;

namespace InternationalVillage_Admin.Component
{
    /// <summary>
    /// Interaction logic for ApartmentUC.xaml
    /// </summary>
    public partial class ApartmentUC : UserControl
    {
        public ApartmentUCViewModel Apartment { get; set; }

        public ApartmentUC()
        {
            InitializeComponent();
            this.DataContext = Apartment = new ApartmentUCViewModel();
        }
    }
}
