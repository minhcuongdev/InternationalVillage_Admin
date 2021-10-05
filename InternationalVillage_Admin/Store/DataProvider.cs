using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using InternationalVillage_Admin.Utilties;
using System.Windows;

namespace InternationalVillage_Admin.Store
{
    class DataProvider
    {
        private static DataProvider instance;
        internal static DataProvider Instance { get { if (instance == null) instance = new DataProvider(); return instance; } set => instance = value; }

        private DataProvider() { }

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();

            MySqlConnection connection = ConnectDB.Instance.CreateConnection();

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(data);
            }
            catch
            {
                MessageBox.Show("Failed connection");
            }
            finally
            {
                connection.Close();
            }
            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            MySqlConnection connection = ConnectDB.Instance.CreateConnection();
            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Failed connection"); }
            finally
            {
                connection.Close();
            }
            return data;
        }
    }
}
