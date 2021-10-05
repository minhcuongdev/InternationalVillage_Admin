using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Utilties
{
    class ConnectDB
    {
        private static ConnectDB instance;

        internal static ConnectDB Instance { get { if (instance == null) instance = new ConnectDB(); return ConnectDB.instance; } set => instance = value; }

        private ConnectDB() { }

        string ConnectionStringMySQL = "Server=bxkd4dmeubxc6fu78uf6-mysql.services.clever-cloud.com;Port=3306;Database=bxkd4dmeubxc6fu78uf6;Uid=uoymuyejhkwn2k39;Pwd=gdazjQfURfzKChTEJxQQ;";

        public MySqlConnection CreateConnection()
        {
            MySqlConnection connection = new MySqlConnection(ConnectionStringMySQL);

            return connection;
        }
    }
}
