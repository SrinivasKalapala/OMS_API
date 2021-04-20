using OMS.Common;
using System.Collections.Generic;

namespace OMS.IDataAccess
{
    public interface IOrdersDataAccess
    {
        int CreateOrder(Order order);
        List<Order> GetOrders();
    }
}
