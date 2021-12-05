using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Model
{
    class DetailBooking
    {
        string name;
        int quantity;
        int price;

        public DetailBooking(string n, int q, int p)
        {
            name = n;
            quantity = q;
            price = p;
        }
        public DetailBooking()
        { }

        public string Name { get => name; set => name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public int Price { get => price; set => price = value; }
    }
}
