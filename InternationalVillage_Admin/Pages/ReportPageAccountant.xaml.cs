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
using LiveCharts;
using LiveCharts.Wpf;

namespace InternationalVillage_Admin.Pages
{
    /// <summary>
    /// Interaction logic for ReportPageAccountant.xaml
    /// </summary>
    public partial class ReportPageAccountant : Page
    {
        public ReportPageAccountant()
        {
            InitializeComponent();
            this.PieChart();
            this.Cartesian();
            this.BarChart();

        }

        //Line Graph
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public void Cartesian()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Luxury",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                },
                new LineSeries
                {
                    Title = "High Standard",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Standard",
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");

            //modifying the series collection will animate and update the chart
            //SeriesCollection.Add(new LineSeries
            //{
            //    Title = "Series 4",
            //    Values = new ChartValues<double> { 5, 3, 2, 4 },
            //    LineSmoothness = 0, //0: straight lines, 1: really smooth lines
            //    PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
            //    PointGeometrySize = 50,
            //    PointForeground = Brushes.Gray
            //});

            //modifying any series values will also animate and update the chart
            //SeriesCollection[3].Values.Add(5d);

            DataContext = this;
        }

        //Pie Chart
        public Func<ChartPoint, string> PointLabel { get; set; }
        public void PieChart()
        {
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            DataContext = this;
        }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        //Bar chart
        public SeriesCollection SeriesCollection_Bar { get; set; }
        public string[] Label_Bar { get; set; }
        public Func<double, string> Formatter { get; set; }
        public void BarChart()
        {
            SeriesCollection_Bar = new SeriesCollection
            {
                new ColumnSeries
                {

                    Values = new ChartValues<double> { 50, 35, 45, 38, 39, 30 }
                }
            };

            //adding series will update and animate the chart automatically
            //SeriesCollection_Bar.Add(new ColumnSeries
            //{
            //    
            //    Values = new ChartValues<double> { 11, 56, 42 }
            //});

            //also adding values updates and animates the chart automatically
            //SeriesCollection_Bar[1].Values.Add(48d);

            Labels = new[] { "Pool", "Gym", "Restaurant", "Golf", "Tennis", "Bar" };
            Formatter = value => value.ToString("N");

            DataContext = this;
        }
    }

}

