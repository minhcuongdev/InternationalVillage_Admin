using InternationalVillage_Admin.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace InternationalVillage_Admin.Store
{
    class IncidentStore
    {
        private static IncidentStore instance;
        internal static IncidentStore Instance { get { if (instance == null) instance = new IncidentStore(); return instance; } set => instance = value; }


        private IncidentStore() { }

        private Incident incidentSelected = null;
        internal Incident IncidentSelected { get => incidentSelected; set => incidentSelected = value; }

        private List<Incident> incidents = new List<Incident>();

        public List<Incident> GetIncidentList()
        {
            List<Incident> list = new List<Incident>();

            string query = "select i.*, c.FullName from Incident as i, Customer as c where i.Id_Customer = c.Id_Customer and Status = 'No accepted';";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow r in dt.Rows)
            {
                Incident i = new Incident(r);
                list.Add(i);
            }

            incidents.Clear();
            incidents = list;
            return list;
        }

        public Incident GetIncidentById(string id)
        {
            for (int i = 0; i < incidents.Count; i++)
            {
                if (id.Equals(incidents[i].IdIncident))
                {
                    return incidents[i];
                }
            }

            return null;
        }

        public bool UpdateState(string idIncident, string idReceptionist)
        {
            string query = string.Format("Update Incident set Status = 'Accepted', Id_Receptionist = '{0}' where Id_Incident = '{1}'",idReceptionist,idIncident);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

    }
}
