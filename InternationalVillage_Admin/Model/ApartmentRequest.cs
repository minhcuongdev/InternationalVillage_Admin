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
        public ApartmentRequest(string id, string idB, string idA, string checkin, string checkout)
        {
            Type = idA;
            ID = id;
            Fullname = idB;
            CheckIn = checkin;
            CheckOut = checkout;
            
        }



        private string iD;
        private string type;
        private string fullname;
        private string checkIn;
        private string checkOut;

        
        public string Type { get => type; set => type = value; }
        public string Fullname { get => fullname; set => fullname = value; }
        public string CheckIn { get => checkIn; set => checkIn = value; }
        public string CheckOut { get => checkOut; set => checkOut = value; }
        public string ID { get => iD; set => iD = value; }
    }
}

