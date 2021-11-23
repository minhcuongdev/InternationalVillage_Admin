using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternationalVillage_Admin.Model;

namespace InternationalVillage_Admin.FakeData
{
    class ExchangeRateData
    {
        private static ExchangeRateData instance;

        internal static ExchangeRateData Instance { get { if (instance == null) instance = new ExchangeRateData(); return instance; } set => instance = value; }

        ExchangeRateData() { }

        public List<ExchangeRate> GetExchangeRateList()
        {
            List<ExchangeRate> list = new List<ExchangeRate>();

            ExchangeRate rate1 = new ExchangeRate("/Image/united-states.png","America", "USD", "1");
            ExchangeRate rate2 = new ExchangeRate("/Image/european-union.png","EURO", "EUR", "0.887");
            ExchangeRate rate3 = new ExchangeRate("/Image/vietnam.png","Vietname", "VND", "22,662");
            ExchangeRate rate4 = new ExchangeRate("/Image/singapore.png","Singapore", "SGD", "1.361");
            ExchangeRate rate5 = new ExchangeRate("/Image/australia.png","Australia", "AUD", "1.382");

            list.Add(rate1);
            list.Add(rate2);
            list.Add(rate3);
            list.Add(rate4);
            list.Add(rate5);


            return list;
        }
    }
}
