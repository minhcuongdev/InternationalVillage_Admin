using System;
using System.Collections.Generic;
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
        

        List<ApartmentRequest> bookingList = FakeData.ApartmentRequestData.Instance.GetBookingList();

        public List<ApartmentRequest> GetBookingList()
        {
            return bookingList;
        }
    }
}
