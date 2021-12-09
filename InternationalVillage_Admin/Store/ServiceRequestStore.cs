using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InternationalVillage_Admin.Model;

namespace InternationalVillage_Admin.Store
{
    class ServiceRequestStore
    {
        private static ServiceRequestStore instance;
        internal static ServiceRequestStore Instance { get { if (instance == null) instance = new ServiceRequestStore(); return instance; } set => instance = value; }

        ServiceRequestStore() { }

        public List<ServiceRequest> GetServiceRequestList()
        {
            List<ServiceRequest> list = new List<ServiceRequest>();

            string query = "select o.*,FullName,ServiceName from OderingServiceTable as o,Customer as c,Service as s where o.Id_Service = s.Id_Service and o.Id_Customer = c.Id_Customer and State = 'No Accept';";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow r in dt.Rows)
            {
                ServiceRequest s = new ServiceRequest(r);
                list.Add(s);
            }

            return list;
        }

        public bool UpdateState(ServiceRequest s)
        {
            string query = string.Format("Update OderingServiceTable set State = 'Accepted' where Id_Customer = '{0}' and Id_Service ='{1}' and CheckInDate = '{2}' and CheckOutDate= '{3}'",s.IdCustomer,s.IdService,s.CheckIn.ToString("yyyy-MM-dd H:mm:ss"),s.CheckOut.ToString("yyyy-MM-dd H:mm:ss"));
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
