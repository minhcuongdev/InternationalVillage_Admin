using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Role { get { return role; } }
        public string IdUser { get { return idUser; } }

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
                return account;
            }
            return null;
        }
    }
}
