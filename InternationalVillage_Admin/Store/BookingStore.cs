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

        public int InsertCustomer(string fullname, string iden, string visa, string avar)
        {
            IdCustomer = CreateIDCustomer();
            string query = string.Format("insert into Customer values ('{0}','{1}','{2}','{3}','{4}');", IdCustomer, fullname, iden, visa, avar);
            return DataProvider.Instance.ExecuteNonQuery(query);
        }

        public string CreateIDCustomer()
        {
            string query = string.Format("select * from Customer;");
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            int count = dt.Rows.Count;

            return "C_" + (count + 1).ToString();
        }
    }
}
