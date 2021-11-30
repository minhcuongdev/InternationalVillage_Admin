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
        public ApartmentRequest(int id, string idB, string idA, string checkin, string checkout)
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
            CheckIn = row["CheckInDate"].ToString();
            CheckOut = row["CheckOutDate"].ToString();
            Quantity = (int)row["Quantity"];
            State = row["State"].ToString();
            BookingTime = row["BookingTime"].ToString();
        }

        private int iD;
        private string type;
        private string fullname;
        private string checkIn;
        private string checkOut;
        private int quantity;
        private string state;
        private string bookingTime;

        
        public string Type { get => type; set => type = value; }
        public string Fullname { get => fullname; set => fullname = value; }
        public string CheckIn { get => checkIn; set => checkIn = value; }
        public string CheckOut { get => checkOut; set => checkOut = value; }
        public int ID { get => iD; set => iD = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string State { get => state; set => state = value; }
        public string BookingTime { get => bookingTime; set => bookingTime = value; }
    }
}

