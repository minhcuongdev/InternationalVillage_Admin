using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternationalVillage_Admin.Model;

namespace InternationalVillage_Admin.Store
{
    class ReportStore
    {
        private static ReportStore instance;
        internal static ReportStore Instance { get { if (instance == null) instance = new ReportStore(); return instance; } set => instance = value; }

        internal Report MakeReport { get => makeReport; set => makeReport = value; }
        public int SumPeople { get => sumPeople; set => sumPeople = value; }
        public int SumRevenue { get => sumRevenue; set => sumRevenue = value; }

        private ReportStore() { }

        private Report makeReport = new Report();
        private int sumPeople;
        private int sumRevenue;
        public DataTable GetPieChartByDay(DateTime start, DateTime due)
        {

            string query = "SELECT DetailApartment.TypeOfApartment,  sum(BookingApartmentTemp.Quantity) AS 'Sum' ,sum(BookingApartmentTemp.NumberPeople) AS 'People'  from DetailApartment left join BookingApartmentTemp "
                             + " on DetailApartment.TypeOfApartment = BookingApartmentTemp.TypeOfApartment "
                             + "where State = 'Accepted' and CheckInDate BETWEEN CAST('" + start.ToString("yyyy-MM-dd H:mm:ss") + "' AS Datetime) AND CAST('" + due.ToString("yyyy-MM-dd H:mm:ss") + "' AS Datetime) "
                             + "group by  DetailApartment.TypeOfApartment; ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public DataTable GetPieChartByMonth(int year)
        {

            string query = "SELECT DetailApartment.TypeOfApartment,  sum(BookingApartmentTemp.Quantity) AS 'Sum' ,sum(BookingApartmentTemp.NumberPeople) AS 'People'  from DetailApartment left join BookingApartmentTemp "
                             + " on DetailApartment.TypeOfApartment = BookingApartmentTemp.TypeOfApartment "
                             + "where State = 'Accepted' and year(CheckInDate) = " + year.ToString() + " "
                             + "group by  DetailApartment.TypeOfApartment; ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public DataTable GetBarChartByDay(DateTime start, DateTime due)
        {

            string query = "SELECT Service.ServiceName,  sum(OderingServiceTable.Quantity) AS 'Sum'  from Service left join OderingServiceTable "
                            + "on Service.Id_Service = OderingServiceTable.Id_Service "
                            + "where State = 'Accepted' and CheckInDate BETWEEN CAST('" + start.ToString("yyyy-MM-dd H:mm:ss") + "' AS Datetime) AND CAST('" + due.ToString("yyyy-MM-dd H:mm:ss") + "' AS Datetime) "
                            + "group by  Service.ServiceName; ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }
        public DataTable GetBarChartByMonth(int year)
        {

            string query = "SELECT Service.ServiceName,  sum(OderingServiceTable.Quantity) AS 'Sum'  from Service left join OderingServiceTable "
                            + "on Service.Id_Service = OderingServiceTable.Id_Service "
                            + "where State = 'Accepted' and year(CheckInDate) = " + year.ToString() + " "
                            + "group by  Service.ServiceName; ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public int[,] GetLineChart(int year)
        {
            int[,] result = new int[3, 13];
            for (int i = 0; i < 3; i++)
                for (int j = 1; j < 13; j++)
                {
                    result[i, j] = 0;
                }

            string query = "SELECT month(DetailApartmentBill.CheckInDate) as 'Month',  sum(DetailApartmentBill.Price) AS 'Sum'  from DetailApartmentBill "
                           + " where year(DetailApartmentBill.CheckInDate) = " + year + " and DetailApartmentBill.Id_Apartment like '3A%' "
                           + " group by  month(DetailApartmentBill.CheckInDate); ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                result[0, Int32.Parse(row["Month"].ToString())] = Int32.Parse(row["Sum"].ToString());
            }
            ////////////////
            string query1 = "SELECT month(DetailApartmentBill.CheckInDate) as 'Month',  sum(DetailApartmentBill.Price) AS 'Sum'  from DetailApartmentBill "
                           + " where year(DetailApartmentBill.CheckInDate) = " + year + " and DetailApartmentBill.Id_Apartment like '3B%' "
                           + " group by  month(DetailApartmentBill.CheckInDate); ";
            DataTable data1 = DataProvider.Instance.ExecuteQuery(query1);

            foreach (DataRow row in data1.Rows)
            {
                result[1, Int32.Parse(row["Month"].ToString())] = Int32.Parse(row["Sum"].ToString());
            }
            ////////////////
            string query2 = "SELECT month(DetailApartmentBill.CheckInDate) as 'Month',  sum(DetailApartmentBill.Price) AS 'Sum'  from DetailApartmentBill "
                           + " where year(DetailApartmentBill.CheckInDate) = " + year + " and DetailApartmentBill.Id_Apartment like '2A%' "
                           + " group by  month(DetailApartmentBill.CheckInDate); ";
            DataTable data2 = DataProvider.Instance.ExecuteQuery(query2);

            foreach (DataRow row in data2.Rows)
            {
                result[2, Int32.Parse(row["Month"].ToString())] = Int32.Parse(row["Sum"].ToString());
            }

            return result;
        }

        public int InsertReportByDay(DateTime start, DateTime due)
        {
            string query = "select * from Report";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            string id = "R_" + (data.Rows.Count + 1);

            string query2 = string.Format("insert into Report values ('{0}','{1}','{2}','{3}',{4},'{5}');",
                                                                         id, "Day", start.ToString("yyyy-MM-dd H:mm:ss"), due.ToString("yyyy-MM-dd H:mm:ss"), "null", "No accapted");
            return DataProvider.Instance.ExecuteNonQuery(query2);
        }

        public int InsertReportByMonth(string year)
        {
            string query = "select * from Report";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            string id = "R_" + (data.Rows.Count + 1);

            string query2 = string.Format("insert into Report values ('{0}','{1}',{2},{3},{4},'{5}');",
                                                                         id, "Month", "null", "null", year, "No accapted");
            return DataProvider.Instance.ExecuteNonQuery(query2);
        }

        public List<Report> GetListReport()
        {
            List<Report> list = new List<Report>();
            string query = "select * from Report";
            ;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Report report = new Report(item);
                list.Add(report);
            }

            return list;
        }

        public string GetTotalRevenueByDay(DateTime start, DateTime due)
        {
            
            string query = "select sum(TotalMoney) as 'Sum' from Bill where Status <> 'Not accepted yet' and CheckInDate BETWEEN CAST('" + start.ToString("yyyy-MM-dd H:mm:ss") + "' AS Datetime) AND CAST('" + due.ToString("yyyy-MM-dd H:mm:ss") + "' AS Datetime); ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count == 0) return "0";
            return data.Rows[0]["Sum"].ToString();
        }
        public string GetTotalRevenueByMonth(int year)
        {

            string query = "select sum(TotalMoney) as 'Sum' from Bill where Status <> 'Not accepted yet' and year(CheckInDate) = " + year.ToString() + ";";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count == 0) return "0";
            return data.Rows[0]["Sum"].ToString();
        }
        public void SetReport(string id, string type, string start, string due, string year)
        {
            makeReport.Id = id;
            makeReport.Type = type;
            makeReport.StartDate = start;
            makeReport.DueDate = due;
            makeReport.Year = year;

        }
    }
}
