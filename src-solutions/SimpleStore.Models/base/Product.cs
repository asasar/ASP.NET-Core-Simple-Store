using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStore.Models
{
    public class Product : IProduct
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public DateTime Created { get; set; }

        public bool EnabledUser { get; set; }

        public string ImageUrl { get; set; }
        
    }
}
