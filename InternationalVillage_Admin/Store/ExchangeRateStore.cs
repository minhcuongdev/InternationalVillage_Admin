using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternationalVillage_Admin.Model;

namespace InternationalVillage_Admin.Store
{
    class ExchangeRateStore
    {
        private static ExchangeRateStore instance;
        internal static ExchangeRateStore Instance { get { if (instance == null) instance = new ExchangeRateStore(); return instance; } set => instance = value; }

        ExchangeRateStore() { }

        public List<ExchangeRate> GetExchangeRateList()
        {
            List<ExchangeRate> exchangerateList = new List<ExchangeRate>();

            ExchangeRate rate1 = new ExchangeRate("/Image/united-states.png", "America", "USD", "1");
            ExchangeRate rate2 = new ExchangeRate("/Image/european-union.png", "EURO", "EUR", "0.887");
            ExchangeRate rate3 = new ExchangeRate("/Image/vietnam.png", "Vietname", "VND", "22,662");
            ExchangeRate rate4 = new ExchangeRate("/Image/singapore.png", "Singapore", "SGD", "1.361");
            ExchangeRate rate5 = new ExchangeRate("/Image/australia.png", "Australia", "AUD", "1.382");

            exchangerateList.Add(rate1);
            exchangerateList.Add(rate2);
            exchangerateList.Add(rate3);
            exchangerateList.Add(rate4);
            exchangerateList.Add(rate5);

            return exchangerateList;
        }


    }
}
