﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternationalVillage_Admin.Utilties
{
    class ChangeTypeApartment
    {
        public ChangeTypeApartment()
        {

        }

        public int ChangeTypeToPrice(string type)
        {
            switch (type)
            {
                case "Normal":
                    return 70;
                case "Luxury":
                    return 110;
                case "Standard":
                    return 90;
                default:
                    return 0;
            }
        }

        public string ChangeTypeOfApartment(string type)
        {
            switch(type)
            {
                case "Normal":
                    return "2A";
                case "Luxury":
                    return "3A";
                case "Standard":
                    return "3B";
                default:
                    return "";
            }
        }
    }
}
