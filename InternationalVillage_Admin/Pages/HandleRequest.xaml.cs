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

using InternationalVillage_Admin.Model;
using InternationalVillage_Admin.Store;

namespace InternationalVillage_Admin.Pages
{
    /// <summary>
    /// Interaction logic for HandleRequest.xaml
    /// </summary>
    public partial class HandleRequest : Page
    {
        public HandleRequest()
        {
            InitializeComponent();
            
        }

        private void ShowDetail_Click(object sender, RoutedEventArgs e)
        {
            ApartmentRequest a = DataGridApartment.SelectedItem as ApartmentRequest;            
            ApartmentRequestStore.Instance.ApartmentRequest = ApartmentRequestStore.Instance.GetApartmentById(int.Parse(a.ID.ToString()));
        }
    }
    
}
