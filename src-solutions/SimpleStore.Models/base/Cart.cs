using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStore.Models
{
    public class Cart
    {

        public List<ProductCart> Products { set; get; }

        public double Total { get; set; }

        public bool SuccessPayment { get; set; }
    }
}
