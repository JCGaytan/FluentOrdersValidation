namespace FluentOrdersValidation.Models
{
    public class Order
    {
        public List<Product> Products { get; set; }
        public DateTime OrderDate { get; set; }
    }
}