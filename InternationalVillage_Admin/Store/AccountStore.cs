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
        string avatar = "";
        string username = "";
        string address = "";
        string idAccount = "";
        public string Role { get { return role; } }
        public string IdUser { get { return idUser; } }

        public string Name { get => name; set => name = value; }
        public string Avatar { get => avatar; set => avatar = value; }
        public string Username { get => username; set => username = value; }
        public string Address { get => address; set => address = value; }

        public bool Authentication(string username, string password)
        {
            string query = "select* from Account where Username = '" + username + "' and Password = '" + password + "' and Role != 'Customer'";
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            return result.Rows.Count > 0;
        }

        public bool UpdateProfile(string fullname, string address)
        {
            switch(role)
            {
                case "Receptionist":
                    string query1 = string.Format("update Receptionist set FullName = '{0}',Address = '{1}' where Id_Receptionist='{2}';", fullname, address, IdUser);
                    return DataProvider.Instance.ExecuteNonQuery(query1) > 0;
                case "Accountant":
                    string query2 = string.Format("update Accountant set FullName = '{0}',Address = '{1}' where Id_Accountant='{2}';", fullname, address, IdUser);
                    return DataProvider.Instance.ExecuteNonQuery(query2) > 0;
                case "Manager":
                    string query3 = string.Format("update Manager set FullName = '{0}',Address = '{1}' where Id_Manager='{2}';", fullname, address, IdUser);
                    return DataProvider.Instance.ExecuteNonQuery(query3) > 0;
                default:
                    return false;
            }
        }

        public bool UpdateUserName(string username)
        {
            string query = string.Format("update Account set Username = '{0}' where Id_Account='{1}';", username, idAccount);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool CheckPassword(string password)
        {
            string query = string.Format("select * from Account where Id_Account = '{0}' and Password = '{1}'", idAccount, password);
            DataTable tb = DataProvider.Instance.ExecuteQuery(query);
            return tb.Rows.Count > 0;
        }

        public bool UpdatePassword(string newPassword)
        {
            string query = string.Format("update Account set Password = '{0}' where Id_Account = '{1}'", newPassword, idAccount);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
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
                Username = account.UserName;
                idAccount = account.IdAccount;

                if (role.Equals("Receptionist"))
                {
                    string query2 = "select * from Receptionist where ID_Receptionist = '" + IdUser+  "'";
                    DataTable data2 = DataProvider.Instance.ExecuteQuery(query2);
                    name = (data2.Rows[0])["FullName"].ToString();
                    avatar = (data2.Rows[0])["Avatar"].ToString();
                    address = (data2.Rows[0])["Address"].ToString();
                }

                if (role.Equals("Accountant"))
                {
                    string query2 = "select * from Accountant where ID_Accountant = '" + IdUser + "'";
                    DataTable data2 = DataProvider.Instance.ExecuteQuery(query2);
                    name = (data2.Rows[0])["FullName"].ToString();
                    avatar = (data2.Rows[0])["Avatar"].ToString();
                    address = (data2.Rows[0])["Address"].ToString();
                }

                if (role.Equals("Manager"))
                {
                    string query2 = "select * from Manager where Id_Manager = '" + IdUser + "'";
                    DataTable data2 = DataProvider.Instance.ExecuteQuery(query2);
                    name = (data2.Rows[0])["FullName"].ToString();
                    avatar = (data2.Rows[0])["Avatar"].ToString();
                    address = (data2.Rows[0])["Address"].ToString();
                }
                return account;
            }
 
            return null;
        }

       
    }
}
