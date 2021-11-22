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

namespace InternationalVillage_Admin.Pages
{
    /// <summary>
    /// Interaction logic for ServiceRequest.xaml
    /// </summary>
    public partial class ServiceRequest : Page
    {
        public ServiceRequest()
        {
            InitializeComponent();
            //List<Service> services = new List<Service>();
            //services.Add(new Service() { FullName = "KhanhQuynh", Type = "Pool", Quantity = 3, Checkin="10/11/2021", Checkout="13/11/2021"});
            
            //DataGridService.ItemsSource = services;
        }
        
    }
    //public class Service
    //{
    //    public string FullName { get; set; }
    //    public string Type { get; set; }
    //    public int Quantity { get; set; }
    //    public string Checkin { get; set; }
    //    public string Checkout { get; set; }

        
    //}
}
