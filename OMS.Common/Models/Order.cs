using System;
using System.Collections.Generic;

namespace OMS.Common
{
    public class Order
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal DeliveryCharges { get; set; }
        public string ShippingMobileNumber { get; set; }
        public string ShippingName { get; set; }
        public string ShippingLine1 { get; set; }
        public string ShippingLine2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingPinCode { get; set; }
        public string BillingName { get; set; }
        public string BillingAddressLine1 { get; set; }
        public string BillingAddressLine2 { get; set; }
        public string BillingAddressCity { get; set; }
        public string BillingAddressState { get; set; }
        public string BillingAddressPinCode { get; set; }

        public List<OrderItems> items { get; set; }
    }
}
