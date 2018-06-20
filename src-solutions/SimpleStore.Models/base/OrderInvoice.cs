using System.Collections.Generic;

namespace SimpleStore.Models
{
    public class OrderInvoice
    {
        public OrderInvoice()
        {
            this.Created = System.DateTime.Now;
            
        }

        public int Id { get; set; }

        public System.DateTime Created { get; set; }

        public SimpleStoreUser Customer { get; set; }

        public ICollection<ProductInvoice> Products { get; set; }
        
        public double TotalPrice { get; set; }

     
        public StatusInformation StatusOrder { get; set; }


        public ICollection<HistoryStatus> HistoryStatusOrders { get; set; }
    }
}