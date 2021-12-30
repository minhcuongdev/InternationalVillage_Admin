using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Model
{
    class Report
    {
        string id;
        string type;
        string startDate;
        string dueDate;
        string year;

        public Report() { }
        public Report(DataRow row)
        {
            id = row["Id_Report"].ToString();
            type = row["Type"].ToString();
            if (row["StartDate"] == null) startDate = "";
            else startDate = row["StartDate"].ToString();
            if (row["DueDate"] == null) dueDate = "";
            else dueDate = row["DueDate"].ToString();
            if (row["Year"] == null) year = "";
            else year = row["Year"].ToString();
        }
        public string Id { get => id; set => id = value; }
        public string Type { get => type; set => type = value; }
        public string StartDate { get => startDate; set => startDate = value; }
        public string DueDate { get => dueDate; set => dueDate = value; }
        public string Year { get => year; set => year = value; }
    }
}
