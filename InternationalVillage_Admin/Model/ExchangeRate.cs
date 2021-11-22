using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Model
{
    class ExchangeRate
    {
        public ExchangeRate(string image,string country, string unit, string value)
        {
            Image = image;
            Country = country;
            Unit = unit;
            Value = value;
        }

        private string image;
        private string country;
        private string unit;
        private string value;

        public string Country { get => country; set => country = value; }
        public string Unit { get => unit; set => unit = value; }
        public string Value { get => value; set => this.value = value; }
        public string Image { get => image; set => image = value; }
    }
}
