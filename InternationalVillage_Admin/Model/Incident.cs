using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Model
{
    class Incident
    {
        public Incident() { }

        public Incident(DataRow row)
        {
            IdIncident = row["Id_Incident"].ToString();
            IdCustomer = row["Id_Customer"].ToString();
            CustomerName = row["FullName"].ToString();
            IdApartment = row["Id_Apartment"].ToString();
            Type = row["Type"].ToString();
            Desc = row["Content"].ToString();
            Level = row["Level"].ToString();
            Status = row["Status"].ToString();
        }

        string idIncident;
        string idCustomer;
        string customerName;
        string idApartment;
        string type;
        string desc;
        string level;
        string status;

        public string IdIncident { get => idIncident; set => idIncident = value; }
        public string IdCustomer { get => idCustomer; set => idCustomer = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public string IdApartment { get => idApartment; set => idApartment = value; }
        public string Type { get => type; set => type = value; }
        public string Desc { get => desc; set => desc = value; }
        public string Level { get => level; set => level = value; }
        public string Status { get => status; set => status = value; }
    }
}
