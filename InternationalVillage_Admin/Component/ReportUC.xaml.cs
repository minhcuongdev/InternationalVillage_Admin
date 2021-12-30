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
using InternationalVillage_Admin.Store;

namespace InternationalVillage_Admin.Component
{
    /// <summary>
    /// Interaction logic for ReportUC.xaml
    /// </summary>
    public partial class ReportUC : UserControl
    {
        string id;
        string type;
        string startDate;
        string dueDate;
        string year;
        
        public ReportUC(string i, string t, string start, string due, string ye)
        {
            InitializeComponent();
            id = i;
            type = t;
            startDate = start;
            dueDate = due;
            year = ye;
            TblType.Text = "By " + type;
            if (type.Equals("Month"))
            {
                TblTitleStart.Text = "Year";
                TblStartDate.Text = year;
                TblTitleDue.Visibility = Visibility.Hidden;
                TblDueDate.Visibility = Visibility.Hidden;
            }
            else
            {
                string date1 = DateTime.Parse(startDate).ToString("MM/dd/yyyy");
                string date2 = DateTime.Parse(dueDate).ToString("MM/dd/yyyy");
                TblStartDate.Text = date1;
                TblDueDate.Text = date2;
         
                
            }

        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            ReportStore.Instance.SetReport(Id, Type, startDate, dueDate, year);
        }
        public string Id { get => id; set => id = value; }
        public string Type { get => type; set => type = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string DueDate { get => dueDate; set => dueDate = value; }
        public string Year { get => year; set => year = value; }
    }
}
