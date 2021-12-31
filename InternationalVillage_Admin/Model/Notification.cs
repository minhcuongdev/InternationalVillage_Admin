using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Model
{
    class Notification
    {
        public Notification()
        {

        }

        public Notification(DataRow row)
        {
            IdUser = row["Id_User"].ToString();
            TimeCreate = (DateTime)row["TimeCreate"];
            Content = row["Content"].ToString();
            Status = row["Status"].ToString();
        }

        private string idUser;
        private DateTime timeCreate;
        private string content;
        private string status;

        public string IdUser { get => idUser; set => idUser = value; }
        public DateTime TimeCreate { get => timeCreate; set => timeCreate = value; }
        public string Content { get => content; set => content = value; }
        public string Status { get => status; set => status = value; }
    }
}
