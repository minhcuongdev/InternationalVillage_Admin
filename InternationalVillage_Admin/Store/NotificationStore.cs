using InternationalVillage_Admin.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InternationalVillage_Admin.Store
{
    class NotificationStore
    {
        private static NotificationStore instance;

        internal static NotificationStore Instance { get { if (instance == null) instance = new NotificationStore(); return instance; } set => instance = value; }

        NotificationStore() { }

        public List<Notification> GetListNotification(string idUser)
        {
            List<Notification> list = new List<Notification>();

            string query = string.Format("Select * from Notification where Id_User = '{0}' and Status = 'Unread';", idUser);
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow r in dt.Rows)
            {
                Notification noti = new Notification(r);
                list.Add(noti);
            }

            return list;
        }

        public bool DeleteNotification(string idUser, DateTime createTime)
        {
            string query = string.Format("update Notification set Status = 'Delete' where Id_User = '{0}' and TimeCreate = '{1}';", idUser, createTime.ToString("yyyy-MM-dd H:mm:ss"));
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        public bool NotificationAcceptedRequisition(string idUser, DateTime createTime,string content)
        {
            string query = string.Format("Insert into Notification values ('{0}','{1}','{2}','{3}');",idUser, createTime.ToString("yyyy-MM-dd H:mm:ss"), content,"Unread");
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
    }
}
