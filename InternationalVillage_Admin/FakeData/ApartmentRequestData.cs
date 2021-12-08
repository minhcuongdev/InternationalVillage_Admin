using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternationalVillage_Admin.Model;

namespace InternationalVillage_Admin.FakeData
{
    class ApartmentRequestData
    {
        private static ApartmentRequestData instance;

        internal static ApartmentRequestData Instance { get { if (instance == null) instance = new ApartmentRequestData(); return instance; } set => instance = value; }

        ApartmentRequestData() { }

        public List<ApartmentRequest> GetBookingList()
        {
            List<ApartmentRequest> list = new List<ApartmentRequest>();

            /*ApartmentRequest book1 = new ApartmentRequest(1, "Minh Cuong", "Normal", "19/10/2021", "23/10/2021");
            ApartmentRequest book2 = new ApartmentRequest(2, "Thanh Tam", "Luxury", "19/10/2021", "23/10/2021");*/
            

            return list;
        }
    }
}
