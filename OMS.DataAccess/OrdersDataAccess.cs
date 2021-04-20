using OMS.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace OMS.DataAccess
{
    public class OrdersDataAccess : DBUtil
    {
        SqlConnection con;
        SqlCommand cmd;

        public int CreateOrder(Order order)
        {
            int orderID=0;
            con = CreateConnection();
            cmd = new SqlCommand("USP_CreateOrder", CreateConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", order.FirstName);
            cmd.Parameters.AddWithValue("@LastName", order.LastName);
            cmd.Parameters.AddWithValue("@OrderAmount", order.OrderAmount);
            cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
            cmd.Parameters.AddWithValue("@Discount", order.Discount);
            cmd.Parameters.AddWithValue("@DeliveryCharges", order.DeliveryCharges);
            cmd.Parameters.AddWithValue("@ShippingMobileNumber", order.ShippingMobileNumber);
            cmd.Parameters.AddWithValue("@ShippingName", order.ShippingName);
            cmd.Parameters.AddWithValue("@ShippingLine1", order.ShippingLine1);
            cmd.Parameters.AddWithValue("@ShippingLine2", order.ShippingLine2);
            cmd.Parameters.AddWithValue("@ShippingCity", order.ShippingCity);
            cmd.Parameters.AddWithValue("@ShippingState", order.ShippingState);
            cmd.Parameters.AddWithValue("@ShippingPinCode", order.ShippingPinCode);
            cmd.Parameters.AddWithValue("@BillingName", order.BillingName);
            cmd.Parameters.AddWithValue("@BillingAddressLine1", order.BillingAddressLine1);
            cmd.Parameters.AddWithValue("@BillingAddressLine2", order.BillingAddressLine2);
            cmd.Parameters.AddWithValue("@BillingAddressCity", order.BillingAddressCity);
            cmd.Parameters.AddWithValue("@BillingAddressState", order.BillingAddressState);
            cmd.Parameters.AddWithValue("@BillingAddressPinCode", order.BillingAddressPinCode);

            SqlDataAdapter Apt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Apt.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                orderID = Convert.ToInt32(row["OrderID"]);
            }
            if (orderID > 0)
            {
                foreach (OrderItems item in order.items)
                {
                    SqlCommand orderItemscmd;
                    orderItemscmd = new SqlCommand("USP_CreateOrderItems", CreateConnection());
                    orderItemscmd.CommandType = CommandType.StoredProcedure;
                    orderItemscmd.Parameters.AddWithValue("@OrderID", item.OrderID);
                    orderItemscmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                    orderItemscmd.Parameters.AddWithValue("@GrossPrice", item.GrossPrice);
                    orderItemscmd.Parameters.AddWithValue("@SellingPrice", item.SellingPrice);
                    orderItemscmd.Parameters.AddWithValue("@Discount", item.Discount);
                    SqlDataAdapter Apt1 = new SqlDataAdapter(orderItemscmd);
                    DataTable dt1 = new DataTable();
                    Apt1.Fill(dt1);
                    CloseConnection();
                }
            }
            return orderID;
        }

        public List<Order> GetOrders(PaginationRequestModel pageModel)
        {
            var ordersList = new List<Order>();
            con = CreateConnection();
            cmd = new SqlCommand("USP_GetOrders", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter Apt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Apt.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                Order order = new Order();
                if (row["Id"] != DBNull.Value)
                    order.Id = (int)row["Id"];
                if (row["FirstName"] != DBNull.Value)
                    order.FirstName = (string)row["FirstName"];
                if (row["LastName"] != DBNull.Value)
                    order.LastName = (string)row["LastName"];
                if (row["OrderDate"] != DBNull.Value)
                    order.OrderDate = (DateTime)row["OrderDate"];
                if (row["OrderAmount"] != DBNull.Value)
                    order.OrderAmount = (Decimal)row["OrderAmount"];
                if (row["Discount"] != DBNull.Value)
                    order.Discount = (Decimal)row["Discount"];
                if (row["DeliveryCharges"] != DBNull.Value)
                    order.DeliveryCharges = (Decimal)row["DeliveryCharges"];
                if (row["ShippingMobileNumber"] != DBNull.Value)
                    order.ShippingMobileNumber=(string)row["ShippingMobileNumber"];
                if (row["ShippingName"] != DBNull.Value)
                    order.ShippingName = (string)row["ShippingName"];
                if (row["ShippingLine1"] != DBNull.Value)
                    order.ShippingLine1 = (string)row["ShippingLine1"];
                if (row["ShippingLine2"] != DBNull.Value)
                    order.ShippingLine2 = (string)row["ShippingLine2"];
                if (row["ShippingCity"] != DBNull.Value)
                    order.ShippingCity = (string)row["ShippingCity"];
                if (row["ShippingState"] != DBNull.Value)
                    order.ShippingState = (string)row["ShippingState"];
                if (row["ShippingPinCode"] != DBNull.Value)
                    order.ShippingPinCode = (string)row["ShippingPinCode"];
                if (row["BillingName"] != DBNull.Value)
                    order.BillingName = (string)row["BillingName"];
                if (row["BillingAddressLine1"] != DBNull.Value)
                    order.BillingAddressLine1 = (string)row["BillingAddressLine1"];
                if (row["BillingAddressLine2"] != DBNull.Value)
                    order.BillingAddressLine2 = (string)row["BillingAddressLine2"];
                if (row["BillingAddressCity"] != DBNull.Value)
                    order.BillingAddressCity = (string)row["BillingAddressCity"];
                if (row["BillingAddressState"] != DBNull.Value)
                    order.BillingAddressState = (string)row["BillingAddressState"];
                if (row["BillingAddressPinCode"] != DBNull.Value)
                    order.BillingAddressPinCode = (string)row["BillingAddressPinCode"];
                ordersList.Add(order);
            }
            CloseConnection();
            return ordersList.Skip(pageModel.PageNumber * pageModel.PageSize).Take(pageModel.PageSize).ToList();
        }
    }
}
