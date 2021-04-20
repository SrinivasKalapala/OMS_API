using OMS.Common;
using OMS.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OMS_API.Controllers
{
    [RoutePrefix("order")]
    public class OrdersController : ApiController
    {
        
        /// <summary>
        /// Creates a new order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        [RouteAttribute("create")]
        public HttpResponseMessage CreateOrder([FromBody] Order order)
        {
            HttpResponseMessage response = null;
            OrdersDataAccess _ordersDataAccess = new OrdersDataAccess();
            int orderID = _ordersDataAccess.CreateOrder(order);
            if (orderID > 0)
                response = Request.CreateResponse(HttpStatusCode.Created,"Order created successfully.");
            else
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Not able to place order.");
            return response;
        }

        /// <summary>
        /// To load all the orders
        /// </summary>
        /// <returns>Returns list of orders</returns>
        [HttpGet]
        public HttpResponseMessage GetOrders([FromUri]PaginationRequestModel pageModel)
        {
            HttpResponseMessage response = null;
            OrdersDataAccess _ordersDataAccess = new OrdersDataAccess();
            List<Order> orderList = _ordersDataAccess.GetOrders(pageModel);
            if (orderList != null && orderList.Count() > 0)
            {
                response = Request.CreateResponse(HttpStatusCode.OK, orderList);
            }
            else
                response = Request.CreateResponse(HttpStatusCode.BadRequest, "Not able to place order.");
            return response;
        }

    }
}
