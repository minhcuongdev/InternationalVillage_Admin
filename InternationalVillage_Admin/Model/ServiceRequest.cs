using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Model
{
    class ServiceRequest
    {
        public ServiceRequest(string idService,int quantity,DateTime checkIn, DateTime checkOut) 
        {
            IdService = idService;
            Quantity = quantity;
            CheckIn = checkIn;
            CheckOut = checkOut;
        }

        public ServiceRequest(DataRow row)
        {
            IdCustomer = row["Id_Customer"].ToString();
            IdService = row["Id_Service"].ToString();
            CheckIn = (DateTime)row["CheckInDate"];
            CheckOut = (DateTime)row["CheckOutDate"];
            RegisterDate = (DateTime)row["RegisterDay"];
            Quantity = (int)row["Quantity"];
            State = row["State"].ToString();
            IdApartment = row["Id_Apartment"].ToString();
            CustomerName = row["FullName"].ToString();
            ServiceName = row["ServiceName"].ToString();
            NumberPeople = (int)row["NumberPeopel"];
            UnitPrice = (int)row["UnitPrice"];
        }

        private string idCustomer;
        private string customerName;
        private string idService;
        private string serviceName;
        private DateTime checkIn;
        private DateTime checkOut;
        private DateTime registerDate;
        private int quantity;
        private string state;
        private string idApartment;
        private int numberPeople;
        private int unitPrice;

        public string IdCustomer { get => idCustomer; set => idCustomer = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public string IdService { get => idService; set => idService = value; }
        public string ServiceName { get => serviceName; set => serviceName = value; }
        public DateTime CheckIn { get => checkIn; set => checkIn = value; }
        public DateTime CheckOut { get => checkOut; set => checkOut = value; }
        public DateTime RegisterDate { get => registerDate; set => registerDate = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string State { get => state; set => state = value; }
        public string IdApartment { get => idApartment; set => idApartment = value; }
        public int NumberPeople { get => numberPeople; set => numberPeople = value; }
        public int UnitPrice { get => unitPrice; set => unitPrice = value; }
    }
}
