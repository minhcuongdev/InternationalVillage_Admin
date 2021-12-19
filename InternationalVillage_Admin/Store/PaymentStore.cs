using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternationalVillage_Admin.Model;

namespace InternationalVillage_Admin.Store
{
    class PaymentStore
    {
        private static PaymentStore instance;
        internal static PaymentStore Instance { get { if (instance == null) instance = new PaymentStore(); return instance; } set => instance = value; }

        public bool isFinished = false;

        private PaymentStore() { }
        string idCustomer;
        string nameCustomer;
        string nameReceptionist;
        string idBill;
        string checkin;
        string checkout;
        string totalMoney;
        string paidMoney;
        string change;
        string status;
        string paydDate;
        List<DetailBooking> listDetailBookings;

        public string IdCustomer { get => idCustomer; set => idCustomer = value; }
        public string IdBill { get => idBill; set => idBill = value; }
        public string NameCustomer { get => nameCustomer; set => nameCustomer = value; }
        public string Checkin { get => checkin; set => checkin = value; }
        public string Checkout { get => checkout; set => checkout = value; }
        public string NameReceptionist { get => nameReceptionist; set => nameReceptionist = value; }
        public string TotalMoney { get => totalMoney; set => totalMoney = value; }
        public string Status { get => status; set => status = value; }
        internal List<DetailBooking> ListDetailBookings { get => listDetailBookings; set => listDetailBookings = value; }
        public string PaydDate { get => paydDate; set => paydDate = value; }
        public string PaidMoney { get => paidMoney; set => paidMoney = value; }
        public string Change { get => change; set => change = value; }

        public bool CreateBill(string idCustoemr,DateTime CheckIn,DateTime CheckOut)
        {
            IdBill = CreateIDBill();
            string query = string.Format("insert into Bill (Id_Bill,Id_Customer,TotalMoney,CheckInDate,CheckOutDate,Status) values ('{0}','{1}',{2},'{3}','{4}','{5}')", IdBill, IdCustomer, 0, CheckIn.ToString("yyyy-MM-dd H:mm:ss"), CheckOut.ToString("yyyy-MM-dd H:mm:ss"),"Not accepted yet");
            int result = DataProvider.Instance.ExecuteNonQuery(query);


            return result > 0;
        }

        public string GetIdBill(string idApartment,DateTime checkIn,DateTime checkOut)
        {
            string query = string.Format("select Id_Bill from DetailApartmentBill where Id_Apartment = '{0}' and CheckInDate = '{1}' and CheckOutDate = '{2}';", idApartment, checkIn.ToString("yyyy-MM-dd H:mm:ss"), checkOut.ToString("yyyy-MM-dd H:mm:ss"));
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            return dt.Rows[0]["Id_Bill"].ToString();
        }

        public bool UpdateToTal(string IdBill)
        {
            string query = "update Bill as b, ( select sum(Price) as sum,Id_Bill from DetailApartmentBill where Id_Bill = '"+IdBill+"' group by Id_Bill) as a set b.TotalMoney = a.sum where b.Id_Bill = '"+IdBill+"'";
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateToTalService(string IdBill)
        {
            string query = "update Bill as b, ( select sum(Price) as sum,Id_Bill from DetailServiceBill where Id_Bill = '" + IdBill + "' group by Id_Bill) as a set b.TotalMoney = a.sum where b.Id_Bill = '" + IdBill + "'";
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool InsertDetailApartmentBill(string idApartment, string idBill, int price,DateTime CheckIn, DateTime CheckOut)
        {
            string query = string.Format("insert into DetailApartmentBill (Id_Apartment,Id_Bill,Price,CheckInDate,CheckOutDate) values ('{0}','{1}',{2},'{3}','{4}')", idApartment, idBill, price,CheckIn.ToString("yyyy-MM-dd H:mm:ss"),CheckOut.ToString("yyyy-MM-dd H:mm:ss"));
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool InsertDetailServiceBill(ServiceRequest r,string idBill,int price)
        {
            string query = string.Format("insert into DetailServiceBill values ('{0}','{1}',{2},'{3}','{4}',{5})",r.IdService,idBill,r.Quantity,r.CheckIn.ToString("yyyy-MM-dd H:mm:ss"),r.CheckOut.ToString("yyyy-MM-dd H:mm:ss"),price);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        string CreateIDBill()
        {
            string query = "select * from Bill";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            int count = 1;
            foreach(DataRow r in dt.Rows)
            {
                count++;
            }

            return "B_" + count.ToString();
        }

        public void FindDetailBill(string id_Bill)
        {
            string query = "select * from Customer,Bill where Bill.Id_Customer = Customer.Id_Customer and Id_Bill = '"+id_Bill+"'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            
            foreach (DataRow item in data.Rows)
            {
                idCustomer = item["Id_Customer"].ToString();
                idBill = item["Id_Bill"].ToString();
                nameCustomer = item["FullName"].ToString();
                nameReceptionist = AccountStore.Instance.Name;
                checkin = item["CheckInDate"].ToString();
                checkout = item["CheckOutDate"].ToString();
                totalMoney = item["TotalMoney"].ToString();
                if (item["Paid"] == null) paidMoney = "";
                else paidMoney = item["Paid"].ToString();
                if (item["Changes"] == null) change = "";
                else change = item["Changes"].ToString();
                status = item["Status"].ToString();
                if (item["PayDate"] == null) paydDate = "";
                else paydDate = item["PayDate"].ToString();

            }

            ListDetailBookings = new List<DetailBooking>();
            string query2 = "select * from DetailApartmentBill where ID_Bill = '" + idBill + "'";
            DataTable data2 = DataProvider.Instance.ExecuteQuery(query2);
            foreach (DataRow item in data2.Rows)
            {
                DetailBooking detailBooking = new DetailBooking(item["Id_Apartment"].ToString(), 1, Int32.Parse(item["Price"].ToString()));
                ListDetailBookings.Add(detailBooking);
            }

            string query3 = "select DetailServiceBill.Price , ServiceName, Quantity from Service, DetailServiceBill where Service.ID_Service = DetailServiceBill.ID_Service and ID_Bill = '"+idBill+"'";
            DataTable data3 = DataProvider.Instance.ExecuteQuery(query3);
            foreach (DataRow item in data3.Rows)
            {
                DetailBooking detailBooking = new DetailBooking(item["ServiceName"].ToString(), Int32.Parse(item["Quantity"].ToString()), Int32.Parse(item["Price"].ToString()));
                ListDetailBookings.Add(detailBooking);
            }

            

        }

        public List<Bill> GetListBill(string infor)
        {
            List<Bill> list = new List<Bill>();
            string query = string.Format("select Id_Bill, CheckinDate, CheckOutDate, Status from Bill,Customer " +
                "where Bill.Id_Customer = Customer.Id_Customer and (Identification like '%{0}%' or Visa like '%{0}%' or FullName like '%{0}%');", infor);

            ;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Bill bill = new Bill(item["Id_Bill"].ToString(), DateTime.Parse(item["CheckInDate"].ToString()).ToString("dd/MM/yyyy"), DateTime.Parse(item["CheckOutDate"].ToString()).ToString("dd/MM/yyyy"), item["Status"].ToString());
                list.Add(bill);
            }

            return list;
        }

        public bool CheckID(string id, string visa)
        {
            string query = "select * from Customer where  (Identification = '" + id + "' or Visa = '" + visa + "')";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            return (data.Rows.Count > 0);
        }


    }
}
