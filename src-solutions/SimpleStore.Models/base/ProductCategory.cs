using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStore.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }

        public int IdProduct { get; set; }

        public int IdCategory { get; set; }
    }
}
