using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Model
{

    internal class Cart
    {
        [Key]
        public int Cart_id { get; set; }

        public int Cust_id { get; set; }

        [Required,Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; } = 0;

        [ForeignKey(nameof(Cust_id))]
        public Users? User { get; set; }

        public ICollection<Cart_item> CartItems { get; set; }
    }

}