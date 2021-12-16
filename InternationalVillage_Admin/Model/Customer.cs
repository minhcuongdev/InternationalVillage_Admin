using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Model
{
    class Customer
    {
        public Customer() { }

        public Customer(DataRow row)
        {
            IdCustomer = row["Id_Customer"].ToString();
            FullName = row["FullName"].ToString();
            Identification = row["Identification"].ToString();
            Visa = row["Visa"].ToString();
        }

        string idCustomer;
        string fullName;
        string identification;
        string visa;

        public string IdCustomer { get => idCustomer; set => idCustomer = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public string Identification { get => identification; set => identification = value; }
        public string Visa { get => visa; set => visa = value; }
    }
}
