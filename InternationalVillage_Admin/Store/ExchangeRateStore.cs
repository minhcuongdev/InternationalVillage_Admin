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

        List<ExchangeRate> exchangerateList = FakeData.ExchangeRateData.Instance.GetExchangeRateList();

        public List<ExchangeRate> GetExchangeRateList()
        {
            return exchangerateList;
        }


    }
}
