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

        public bool InsertBookingApartmentTable(string idCustomre, string idApartment,int price, DateTime CheckIn, DateTime CheckOut)
        {
            string query = string.Format("insert into BookingApartmentTable values ('{0}','{1}','{2}','{3}',{4});", idCustomre, idApartment, CheckIn.ToString("yyyy-MM-dd H:mm:ss"), CheckOut.ToString("yyyy-MM-dd H:mm:ss"), price);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public List<Apartment> GetAvailableList(string typeOfApartment, DateTime CheckIn)
        {
            List<Apartment> list = new List<Apartment>();

            string query = string.Format("select * from Apartment where Id_Apartment not in (SELECT Id_Apartment FROM DetailApartmentBill where CheckInDate = '{0}') and Status != 'incident' and TypeOfApartment = '{1}';",CheckIn.ToString("yyyy-MM-dd H:mm:ss") ,typeOfApartment);
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
