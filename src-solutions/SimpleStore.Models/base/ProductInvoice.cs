namespace SimpleStore.Models
{
    public class ProductInvoice
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public double Qty { get; set; }
    }
}