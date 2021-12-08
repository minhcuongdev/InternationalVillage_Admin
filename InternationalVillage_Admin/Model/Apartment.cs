using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Model
{
    class Apartment
    {
        public Apartment() { }

        public Apartment(DataRow row)
        {
            Id = row["Id_Apartment"].ToString();
            TypeOfApartment = row["TypeOfApartment"].ToString();
        }

        string id;
        string typeOfApartment;

        public string Id { get => id; set => id = value; }
        public string TypeOfApartment { get => typeOfApartment; set => typeOfApartment = value; }
    }
}
