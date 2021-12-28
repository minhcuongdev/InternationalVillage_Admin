using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InternationalVillage_Admin.Component;

namespace InternationalVillage_Admin.ViewModel
{
    class ListofReportViewModel : BaseViewModel
    {
        public ICommand LoadReport { get; set; }

        public ListofReportViewModel()
        {
            LoadReport = new RelayCommand<StackPanel>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < 20; i++)
                {
                    ReportUC reportUC = new ReportUC();                  
                    p.Children.Add(reportUC);
                }
            });
        }
    }
}
