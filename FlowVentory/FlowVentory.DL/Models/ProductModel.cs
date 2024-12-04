namespace Flowventory.DL.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public int Sales { get; set; }
        public int Stock { get; set; }
    }
}