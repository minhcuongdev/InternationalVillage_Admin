using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

using InternationalVillage_Admin.Store;
using InternationalVillage_Admin.Model;

namespace InternationalVillage_Admin.ViewModel
{
    class IncidentRequestViewModel : BaseViewModel
    {
        public ICommand LoadTable { get; set; }

        public IncidentRequestViewModel()
        {
            LoadTable = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                List<Incident> list = IncidentStore.Instance.GetIncidentList();
                p.ItemsSource = list;
            });
        }
    }
}
