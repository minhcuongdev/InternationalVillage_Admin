using InternationalVillage_Admin.ViewModel;
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

namespace InternationalVillage_Admin.Component
{
    /// <summary>
    /// Interaction logic for NotificationUC.xaml
    /// </summary>
    public partial class NotificationUC : UserControl
    {
        public NotificationUCViewModel Notification { get; set; }

        public NotificationUC()
        {
            InitializeComponent();

            this.DataContext = Notification = new NotificationUCViewModel();
        }
    }
}
