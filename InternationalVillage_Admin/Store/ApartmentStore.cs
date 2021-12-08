using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InternationalVillage_Admin.Component;

using InternationalVillage_Admin.Model;

namespace InternationalVillage_Admin.Store
{
    class ApartmentStore
    {
        static private ApartmentStore instance;
        internal static ApartmentStore Instance { get { if (instance == null) instance = new ApartmentStore(); return instance; } set => instance = value; }


        ApartmentStore() { }

        private List<ApartmentUC> apartmentSlectedList = new List<ApartmentUC>();
        public List<ApartmentUC> ApartmentSlectedList { get => apartmentSlectedList; set => apartmentSlectedList = value; }

        public List<Apartment> GetAvailableList(string typeOfApartment)
        {
            List<Apartment> list = new List<Apartment>();

            string query = string.Format("select * from Apartment where Id_Apartment not in (SELECT Id_Apartment FROM DetailApartmentBill where CheckInDate = '{0}') and Status != 'incident' and TypeOfApartment = '{1}';",ApartmentRequestStore.Instance.ApartmentRequest.CheckIn.ToString("yyyy-MM-dd H:mm:ss") ,typeOfApartment);
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow row in dt.Rows)
            {
                Apartment apartment = new Apartment(row);
                list.Add(apartment);
            }

            return list;
        }

        public List<Apartment> GetTakenList(string typeOfApartment)
        {
            List<Apartment> list = new List<Apartment>();

            string query = string.Format("select * from Apartment where Id_Apartment in (SELECT Id_Apartment FROM DetailApartmentBill where CheckInDate = '{0}') and Status != 'incident' and TypeOfApartment = '{1}';", ApartmentRequestStore.Instance.ApartmentRequest.CheckIn.ToString("yyyy-MM-dd H:mm:ss"), typeOfApartment);
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                Apartment apartment = new Apartment(row);
                list.Add(apartment);
            }

            return list;
        }

        public List<Apartment> GetIncidentList(string typeOfApartment)
        {
            List<Apartment> list = new List<Apartment>();

            string query = string.Format("select * from Apartment where TypeOfApartment = '{0}' and Status = 'incident';", typeOfApartment);
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in dt.Rows)
            {
                Apartment apartment = new Apartment(row);
                list.Add(apartment);
            }

            return list;
        }
    }
}
