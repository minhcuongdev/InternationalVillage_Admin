using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Model;
using InternationalVillage_Admin.Pages;
using System.Windows;
using LiveCharts.Wpf;
using LiveCharts;
using LiveCharts.Defaults;

namespace InternationalVillage_Admin.ViewModel
{
    class ReportPageViewModel
    {
        public ICommand LoadPieChart { get; set; }

        private SeriesCollection seriesCollection { get; set; }
        public Func<ChartPoint, string> PointLabel { get; set; }
        public ReportPageViewModel()
        {
            LoadPieChart = new RelayCommand<PieChart>((p) => { return true; }, (p) =>
            {
                SetSeriesCollection(10, 10, 20);
                p.Series = seriesCollection;
                PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                
                MessageBox.Show("alo");
            });
        }

        public void SetSeriesCollection(int Lu, int High, int Stand)
        {
            seriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title="Luxury",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(Lu)},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title="High Standard",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(High)},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title="Standard",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(Stand)},
                    DataLabels = true
                }
            };
        }
    }
}
