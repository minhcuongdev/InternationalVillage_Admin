using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InternationalVillage_Admin.Component;
using InternationalVillage_Admin.Model;
using InternationalVillage_Admin.Store;

namespace InternationalVillage_Admin.ViewModel
{
    class ListofReportViewModel : BaseViewModel
    {
        public ICommand LoadReport { get; set; }
        //public ICommand LoadReport { get; set; }

        public ListofReportViewModel()
        {
            LoadReport = new RelayCommand<StackPanel>((p) => { return true; }, (p) =>
            {
                List<Report> list = ReportStore.Instance.GetListReport();
                for (int i = list.Count-1; i >= 0 ; i--)
                {
                    ReportUC reportUC = new ReportUC(list[i].Id, list[i].Type, list[i].StartDate, list[i].DueDate, list[i].Year);                  
                    p.Children.Add(reportUC);
                }
            });
        }
    }
}
