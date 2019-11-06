using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class FoodMenu
    {
        public int FoodMenuId{ get; set; }

        [Required]
        [StringLength(200)]
        public int Name { get; set; }
        
        [Required]
        public string Description { get; set; }

        public decimal Price { get; set; }

    }
}
