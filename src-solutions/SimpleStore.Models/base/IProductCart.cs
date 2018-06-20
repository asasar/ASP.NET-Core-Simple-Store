namespace SimpleStore.Models
{
    public interface IProductCart: IProduct
    {
        int Qty { get; set; }
    }
}