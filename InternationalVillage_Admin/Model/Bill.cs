using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Model
{
    class Bill
    {
        string id;
        string checkindate;
        string checkoutdate;
        string status;

        public Bill(string idd, string checkin, string checkout, string sta)
        {
            Id = idd;
            Checkindate = checkin;
            Checkoutdate = checkout;
            if (sta.Equals("Not accepted yet")) Status = "Unpaid";
            else Status = "Paid";
        }

        public Bill()
        {

        }

        public string Checkindate { get => checkindate; set => checkindate = value; }
        public string Checkoutdate { get => checkoutdate; set => checkoutdate = value; }
        public string Id { get => id; set => id = value; }
        public string Status { get => status; set => status = value; }
    }
}
