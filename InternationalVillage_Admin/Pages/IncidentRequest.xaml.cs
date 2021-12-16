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
    /// Interaction logic for IncidentRequest.xaml
    /// </summary>
    public partial class IncidentRequest : Page
    {
        public IncidentRequest()
        {
            InitializeComponent();
            
        }

        private void SeeMore(object sender, RoutedEventArgs e)
        {
            Incident incident = DataGridIncident.SelectedItem as Incident;
            IncidentStore.Instance.IncidentSelected = IncidentStore.Instance.GetIncidentById(incident.IdIncident);
            Page.NavigationService.Navigate(new Uri("/Pages/IncidentRequestDetail.xaml", UriKind.RelativeOrAbsolute));
        }
    }
    
}
