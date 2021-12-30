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
using InternationalVillage_Admin.Store;
using LiveCharts.Defaults;
using System.Data;

namespace InternationalVillage_Admin.Pages
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
            this.LoadChart();

        }

        public void LoadChart()
        {
            if (ReportStore.Instance.MakeReport.Type.Equals("Day"))
            {
                lineChart.Visibility = Visibility.Hidden;
                int lu = 0; int high = 0; int stand = 0;
                int sumPeople = 0;
                DateTime startdate = DateTime.Parse(ReportStore.Instance.MakeReport.StartDate);
                DateTime duedate = DateTime.Parse(ReportStore.Instance.MakeReport.DueDate);
                DataTable data = ReportStore.Instance.GetPieChartByDay(startdate,duedate);
                foreach (DataRow row in data.Rows)
                {
                    if (row["TypeOfApartment"].ToString().Equals("3A") && row["Sum"] != null) lu = Int32.Parse(row["Sum"].ToString());
                    else if (row["TypeOfApartment"].ToString().Equals("3B") && row["Sum"] != null) high = Int32.Parse(row["Sum"].ToString());
                    else if (row["TypeOfApartment"].ToString().Equals("2A") && row["Sum"] != null) stand = Int32.Parse(row["Sum"].ToString());
                    sumPeople += Int32.Parse(row["People"].ToString());
                }
                PieChart(lu, high, stand);
                TblTotalCustomer.Text = sumPeople.ToString();

                int Pool = 0; int Gym = 0; int Restaurant = 0; int Golf = 0; int Tennis = 0; int Bar = 0;
                DataTable data2 = ReportStore.Instance.GetBarChartByDay(startdate, duedate);
                foreach (DataRow row in data2.Rows)
                {
                    if (row["ServiceName"].ToString().Equals("Pool") && row["Sum"] != null) Pool = Int32.Parse(row["Sum"].ToString());
                    else if (row["ServiceName"].ToString().Equals("Gym") && row["Sum"] != null) Gym = Int32.Parse(row["Sum"].ToString());
                    else if (row["ServiceName"].ToString().Equals("Restaurant") && row["Sum"] != null) Restaurant = Int32.Parse(row["Sum"].ToString());
                    else if (row["ServiceName"].ToString().Equals("Tennis") && row["Sum"] != null) Tennis = Int32.Parse(row["Sum"].ToString());
                    else if (row["ServiceName"].ToString().Equals("Golf") && row["Sum"] != null) Golf = Int32.Parse(row["Sum"].ToString());
                    else if (row["ServiceName"].ToString().Equals("Bar") && row["Sum"] != null) Bar = Int32.Parse(row["Sum"].ToString());
                }
                BarChart(Pool, Gym, Restaurant, Golf, Tennis, Bar);

                TblTotalRenenue.Text = "$" + ReportStore.Instance.GetTotalRevenueByDay(startdate, duedate);

            }
            else
            {
                int year = Int32.Parse(ReportStore.Instance.MakeReport.Year);
                lineChart.Visibility = Visibility.Visible;
                int lu = 0; int high = 0; int stand = 0;
                int sumPeople = 0;
                DataTable data = ReportStore.Instance.GetPieChartByMonth(year);
                foreach (DataRow row in data.Rows)
                {
                    if (row["TypeOfApartment"].ToString().Equals("3A") && row["Sum"] != null) lu = Int32.Parse(row["Sum"].ToString());
                    else if (row["TypeOfApartment"].ToString().Equals("3B") && row["Sum"] != null) high = Int32.Parse(row["Sum"].ToString());
                    else if (row["TypeOfApartment"].ToString().Equals("2A") && row["Sum"] != null) stand = Int32.Parse(row["Sum"].ToString());
                    sumPeople += Int32.Parse(row["People"].ToString());
                }
                PieChart(lu, high, stand);
                TblTotalCustomer.Text = sumPeople.ToString();

                int Pool = 0; int Gym = 0; int Restaurant = 0; int Golf = 0; int Tennis = 0; int Bar = 0;
                DataTable data2 = ReportStore.Instance.GetBarChartByMonth(year);
                foreach (DataRow row in data2.Rows)
                {
                    if (row["ServiceName"].ToString().Equals("Pool") && row["Sum"] != null) Pool = Int32.Parse(row["Sum"].ToString());
                    else if (row["ServiceName"].ToString().Equals("Gym") && row["Sum"] != null) Gym = Int32.Parse(row["Sum"].ToString());
                    else if (row["ServiceName"].ToString().Equals("Restaurant") && row["Sum"] != null) Restaurant = Int32.Parse(row["Sum"].ToString());
                    else if (row["ServiceName"].ToString().Equals("Tennis") && row["Sum"] != null) Tennis = Int32.Parse(row["Sum"].ToString());
                    else if (row["ServiceName"].ToString().Equals("Golf") && row["Sum"] != null) Golf = Int32.Parse(row["Sum"].ToString());
                    else if (row["ServiceName"].ToString().Equals("Bar") && row["Sum"] != null) Bar = Int32.Parse(row["Sum"].ToString());
                }
                BarChart(Pool, Gym, Restaurant, Golf, Tennis, Bar);

                TblTotalRenenue.Text = "$" + ReportStore.Instance.GetTotalRevenueByMonth(year);

                int[,] data3 = ReportStore.Instance.GetLineChart(year);
                ChartValues<double> luxury = new ChartValues<double>();
                ChartValues<double> highstand = new ChartValues<double>();
                ChartValues<double> standard = new ChartValues<double>();
                List<string> lable = new List<string>();

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 1; j < 13; j++)
                        Console.Write(data3[i, j] + " ");
                    Console.WriteLine();
                }
                if (year == DateTime.Now.Year)
                {
                    for (int j = 1; j <= DateTime.Now.Month; j++)
                    {
                        luxury.Add(data3[0, j]);
                        highstand.Add(data3[1, j]);
                        standard.Add(data3[2, j]);
                        lable.Add((j).ToString());
                    }
                }
                else
                {
                    for (int j = 1; j <= 12; j++)
                    {
                        luxury.Add(data3[0, j]);
                        highstand.Add(data3[1, j]);
                        standard.Add(data3[2, j]);
                        lable.Add((j).ToString());
                    }
                }

                LineChart(luxury, highstand, standard, lable);
            }
        }
        
        string type;
        //Line Graph
        public SeriesCollection SeriesCollection_Line { get; set; }

        public string[] Labels { get; set; }

        //public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public void LineChart(ChartValues<double> lu, ChartValues<double> high, ChartValues<double> stand, List<string> label)
        {

            SeriesCollection_Line = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Luxury",
                    Values = lu,
                },
                new LineSeries
                {
                    Title = "High Standard",
                    Values = high,
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Standard",
                    Values = stand,
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
                }
            };


            Labels = label.ToArray();
            YFormatter = value => value.ToString("C");
            lineChart.Series = SeriesCollection_Line;
            DataContext = this;

        }

        //Pie Chart
        public SeriesCollection SeriesCollection_Pie { get; set; }
        public Func<ChartPoint, string> PointLabel { get; set; }
        public void PieChart(int Lu, int High, int Stand)
        {
            SetSeriesCollection_Pie(Lu, High, Stand);
            pieChart.Series = SeriesCollection_Pie;
            DataContext = this;
        }

        public void SetSeriesCollection_Pie(int Lu, int High, int Stand)
        {
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            SeriesCollection_Pie = new SeriesCollection
            {
                new PieSeries
                {
                    Title="Luxury",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(Lu)},
                    LabelPoint = PointLabel,
                    DataLabels = true
                },
                new PieSeries
                {
                    Title="High Standard",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(High)},
                    LabelPoint = PointLabel,
                    DataLabels = true
                },
                new PieSeries
                {
                    Title="Standard",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(Stand)},
                    LabelPoint = PointLabel,
                    DataLabels = true
                }
            };
        }
        private void pieChart_DataClick(object sender, ChartPoint chartPoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartPoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartPoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        //Bar chart
        public SeriesCollection SeriesCollection_Bar { get; set; }
        public string[] Label_Bar { get; set; }
        public Func<int, string> Formatter { get; set; }
        public void BarChart(int Pool, int Gym, int Restaurant, int Golf, int Tennis, int Bar)
        {
            SetSeriesCollection_Bar(Pool, Gym, Restaurant, Golf, Tennis, Bar);
            barChart.Series = SeriesCollection_Bar;
            DataContext = this;
        }

        public void SetSeriesCollection_Bar(int Pool, int Gym, int Restaurant, int Golf, int Tennis, int Bar)
        {
            SeriesCollection_Bar = new SeriesCollection
            {
                new ColumnSeries
                {

                    Values = new ChartValues<double> {  Pool, Gym,  Restaurant,  Golf,  Tennis,  Bar }
                }
            };
            Label_Bar = new[] { "Pool", "Gym", "Restaurant", "Golf", "Tennis", "Bar" };
            Formatter = value => value.ToString("N");
        }


      
    }
}


