namespace OMS.Common
{
    public class OrderItems
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public string ItemName { get; set; }
        public decimal GrossPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Discount { get; set; }
    }
}
