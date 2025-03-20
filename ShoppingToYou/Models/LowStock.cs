namespace Database.Models
{
    public class LowStock
    {
        public int ID { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public int StockQuantity { get; set; }
        public int DefaultOrderQuantity { get; set; }
    }
}
