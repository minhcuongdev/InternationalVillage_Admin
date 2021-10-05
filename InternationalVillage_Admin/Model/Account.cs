using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Model
{
    class Account
    {
        public Account(string idA, string user, string pass, string r, string idU)
        {
            IdAccount = idA;
            UserName = user;
            Password = pass;
            Role = r;
            IdUser = idU;
        }

        public Account(DataRow row)
        {
            IdAccount = row["Id_Account"].ToString();
            UserName = row["Username"].ToString();
            Password = row["Password"].ToString();
            Role = row["Role"].ToString();
            IdUser = row["Id_User"].ToString();
        }

        private string idAccount;
        private string userName;
        private string password;
        private string role;
        private string idUser;

        public string IdAccount { get => idAccount; set => idAccount = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Role { get => role; set => role = value; }
        public string IdUser { get => idUser; set => idUser = value; }
    }
}
