using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStore.ViewModels
{
    public class ProductViewModel
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength:100, MinimumLength = 3)]
        public string Description { get; set; }
        
        public double Price { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
        public bool EnabledUser { get; set; }

        public string ImageUrl { get; set; }

    }
}
