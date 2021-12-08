using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternationalVillage_Admin.Model;

namespace InternationalVillage_Admin.Store
{
    class ApartmentRequestStore
    {

        private static ApartmentRequestStore instance;
        internal static ApartmentRequestStore Instance { get { if (instance == null) instance = new ApartmentRequestStore(); return instance; } set => instance = value; }


        ApartmentRequestStore() { }

        ApartmentRequest apartmentRequest = null;
        internal ApartmentRequest ApartmentRequest { get => apartmentRequest; set => apartmentRequest = value; }

        List<ApartmentRequest> bookingList = new List<ApartmentRequest>();
        internal List<ApartmentRequest> BookingList { get => bookingList; set => bookingList = value; }

        public ApartmentRequest GetApartmentById(int id)
        {
            for(int i=0;i<bookingList.Count;i++)
            {
                if(id == bookingList[i].ID)
                {
                    return bookingList[i];
                }
            }

            return null;
        }

        public bool UpdateState()
        {
            string query = "Update BookingApartmentTemp set State = 'Accepted' where Id_Customer = '" + ApartmentRequest.IdCustomer + "'";
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public List<ApartmentRequest> GetBookingList()
        {
            List<ApartmentRequest> list = new List<ApartmentRequest>();

            string query = "select Customer.Id_Customer,FullName,Description,CheckInDate,CheckOutDate,Quantity,State,BookingTime from BookingApartmentTemp, Customer, DetailApartment where BookingApartmentTemp.Id_Customer = Customer.Id_Customer and BookingApartmentTemp.TypeOfApartment = DetailApartment.TypeOfApartment and State = 'No Accept'; ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            int id = 1;
            foreach(DataRow dt in data.Rows)
            {
                ApartmentRequest a = new ApartmentRequest(dt, id);
                list.Add(a);
                id++;
            }

            BookingList.Clear();
            BookingList = list;

            return list;
        }
    }
}
