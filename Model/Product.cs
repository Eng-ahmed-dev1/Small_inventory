using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Model
{
    internal class Product
    {
        [Key]
        public int Pro_id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required,Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int InStockQuantity { get; set; }
        public ICollection<Cart_item>? CartItems { get; set; }
    }

}
