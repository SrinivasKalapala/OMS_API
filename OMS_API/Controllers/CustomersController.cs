using OMS.Common;
using OMS.DataAccess;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OMS_API.Controllers
{
    [RoutePrefix("Customer")]
    public class CustomersController : ApiController
    {
        
        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [RouteAttribute("create")]
        public HttpResponseMessage CreateCustomer([FromBody]Customer customer)
        {
            HttpResponseMessage response = null;
            CustomerDataAccess _customerDataAccess = new CustomerDataAccess();
            int rowseffected= _customerDataAccess.CreateCustomer(customer);
            if(rowseffected>0)
                response = Request.CreateResponse(HttpStatusCode.Created);
            else
                response = Request.CreateResponse(HttpStatusCode.NotAcceptable,"Customer with same email already exists");
            return response;
        }
    }
}
