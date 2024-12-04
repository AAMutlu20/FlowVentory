namespace Flowventory.DL.Models
{
    public class Order
    {
        public int ID { get; set; }
        public ICollection<Product> ProductsInOrder { get; set; }
        public decimal TotalOrder { get; set; }
        public string CategoryOrder { get; set; }
    }
}