using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InternationalVillage_Admin.Model;

namespace InternationalVillage_Admin.Store
{
    class AccountStore
    {
        private static AccountStore instance;
        internal static AccountStore Instance { get { if (instance == null) instance = new AccountStore(); return instance; } set => instance = value; }



        private AccountStore() { }

        string role = "";
        string idUser = "";
        string name = "";
        public string Role { get { return role; } }
        public string IdUser { get { return idUser; } }

        public string Name { get => name; set => name = value; }

        public bool Authentication(string username, string password)
        {
            string query = "select* from Account where Username = '" + username + "' and Password = '" + password + "' and Role != 'Customer'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result.Rows.Count > 0;
        }

        // Test get data
        public Account GetAccount(string username, string password)
        { 

            string query = "select * from Account where Username = '" + username + "' and Password = '" + password + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Account account = new Account(item);
                role = account.Role;
                idUser = account.IdUser;

                if (role.Equals("Receptionist"))
                {
                    string query2 = "select * from Receptionist where ID_Receptionist = '" + IdUser+  "'";
                    DataTable data2 = DataProvider.Instance.ExecuteQuery(query2);
                    name = (data2.Rows[0])["FullName"].ToString();
                    
                }
                if (role.Equals("Manager"))
                {
                    string query3 = "select * from Manager where Id_Manager = '" + IdUser + "'";
                    DataTable data3 = DataProvider.Instance.ExecuteQuery(query3);
                    name = (data3.Rows[0])["FullName"].ToString();
                    
                }
                return account;
            }
 
            return null;
        }

       
    }
}
