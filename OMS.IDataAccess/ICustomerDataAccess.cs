using OMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.IDataAccess
{
    public interface ICustomerDataAccess
    {
        int CreateCustomer(Customer customer);
    }
}
