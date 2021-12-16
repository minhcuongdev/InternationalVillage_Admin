using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InternationalVillage_Admin.Model;

namespace InternationalVillage_Admin.Store
{
    class BookingStore
    {
        private static BookingStore instance;
        internal static BookingStore Instance { get { if (instance == null) instance = new BookingStore(); return instance; } set => instance = value; }

        private BookingStore() { }

        public string IdCustomer = "";
        public DateTime CheckIn;
        public DateTime CheckOut;

        public List<Customer> GetCustomerList(string info)
        {
            List<Customer> list = new List<Customer>();

            string query = string.Format("select * from Customer where Identification like '%{0}%' or FullName like '%{0}%' or Visa like '%{0}%';",info);
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow r in dt.Rows)
            {
                Customer c = new Customer(r);
                list.Add(c);
            }

            return list;
        }
    }
}
