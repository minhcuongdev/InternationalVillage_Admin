using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Store
{
    class ReportStore
    {
        private static ReportStore instance;
        internal static ReportStore Instance { get { if (instance == null) instance = new ReportStore(); return instance; } set => instance = value; }
        private ReportStore() { }

        public DataTable GetPieChartByDay(DateTime start, DateTime due)
        {
            
            string query = "SELECT DetailApartment.TypeOfApartment,  sum(BookingApartmentTemp.Quantity) AS 'Sum'  from DetailApartment left join BookingApartmentTemp "
                             + " on DetailApartment.TypeOfApartment = BookingApartmentTemp.TypeOfApartment "
                             + "where State = 'Accepted' and CheckInDate BETWEEN CAST('"+ start.ToString("yyyy-MM-dd H:mm:ss")+"' AS Datetime) AND CAST('"+ due.ToString("yyyy-MM-dd H:mm:ss") + "' AS Datetime) "
                             + "group by  DetailApartment.TypeOfApartment; ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return data;
        }

        public DataTable GetPieChartByMonth(int year)
        {

            string query = "SELECT DetailApartment.TypeOfApartment,  sum(BookingApartmentTemp.Quantity) AS 'Sum'  from DetailApartment left join BookingApartmentTemp "
                             + " on DetailApartment.TypeOfApartment = BookingApartmentTemp.TypeOfApartment "
                             + "where State = 'Accepted' and year(CheckInDate) = "+year.ToString()+" "
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
            for (int i =0; i < 3; i++)
               for (int j = 1; j <13; j++)
                {
                    result[i, j] = 0;
                }

            string query = "SELECT month(DetailApartmentBill.CheckInDate) as 'Month',  sum(DetailApartmentBill.Price) AS 'Sum'  from DetailApartmentBill "
                           + " where year(DetailApartmentBill.CheckInDate) = "+year+" and DetailApartmentBill.Id_Apartment like '3A%' "
                           +  " group by  month(DetailApartmentBill.CheckInDate); ";
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
    }
}
