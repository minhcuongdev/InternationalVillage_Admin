using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

using InternationalVillage_Admin.Store;

namespace InternationalVillage_Admin.ViewModel
{
    class IncidentDetailViewModel : BaseViewModel
    {
        public ICommand LoadId { get; set; }
        public ICommand LoadFullName { get; set; }
        public ICommand LoadReceptionist { get; set; }
        public ICommand LoadApartment { get; set; }
        public ICommand LoadType { get; set; }
        public ICommand LoadLevel { get; set; }
        public ICommand LoadDescription { get; set; }

        public ICommand Confirm { get; set; }

        public IncidentDetailViewModel()
        {
            LoadId = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = IncidentStore.Instance.IncidentSelected.IdIncident;
            });

            LoadFullName = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = IncidentStore.Instance.IncidentSelected.CustomerName;
            });

            LoadReceptionist = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = AccountStore.Instance.Name;
            });

            LoadApartment = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = IncidentStore.Instance.IncidentSelected.IdApartment;
            });

            LoadType = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = IncidentStore.Instance.IncidentSelected.Type;
            });

            LoadLevel = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = IncidentStore.Instance.IncidentSelected.Level;
            });

            LoadDescription = new RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                p.Text = IncidentStore.Instance.IncidentSelected.Desc;
            });

            Confirm = new RelayCommand<Page>((p) => { return true; }, (p) =>
            {
                if(IncidentStore.Instance.UpdateState(IncidentStore.Instance.IncidentSelected.IdIncident, AccountStore.Instance.IdUser))
                {
                    NotificationStore.Instance.NotificationAcceptedRequisition(IncidentStore.Instance.IncidentSelected.IdCustomer, DateTime.Now, "Incident has been solved");
                    p.NavigationService.Navigate(new Uri("/Pages/Incidentrequest.xaml", UriKind.RelativeOrAbsolute));
                }
            });
        }
    }
}
