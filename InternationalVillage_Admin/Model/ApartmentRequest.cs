using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Model
{
    class ApartmentRequest
    {
        public ApartmentRequest(int id, string idB, string idA, DateTime checkin, DateTime checkout)
        {
            ID = id;
            Fullname = idB;
            Type = idA;
            CheckIn = checkin;
            CheckOut = checkout;
            
        }

        public ApartmentRequest(DataRow row,int id)
        {
            ID = id;
            Fullname = row["FullName"].ToString();
            Type = row["Description"].ToString();
            CheckIn = (DateTime)row["CheckInDate"];
            CheckOut = (DateTime)row["CheckOutDate"];
            Quantity = (int)row["Quantity"];
            State = row["State"].ToString();
            BookingTime = row["BookingTime"].ToString();
            IdCustomer = row["Id_Customer"].ToString();
        }

        private int iD;
        private string type;
        private string fullname;
        private DateTime checkIn;
        private DateTime checkOut;
        private int quantity;
        private string state;
        private string bookingTime;
        private string idCustomer;

        
        public string Type { get => type; set => type = value; }
        public string Fullname { get => fullname; set => fullname = value; }
        public DateTime CheckIn { get => checkIn; set => checkIn = value; }
        public DateTime CheckOut { get => checkOut; set => checkOut = value; }
        public int ID { get => iD; set => iD = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string State { get => state; set => state = value; }
        public string BookingTime { get => bookingTime; set => bookingTime = value; }
        public string IdCustomer { get => idCustomer; set => idCustomer = value; }
    }
}

