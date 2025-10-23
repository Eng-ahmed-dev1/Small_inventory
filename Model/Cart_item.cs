using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Model
{

    internal class Cart_item
    {
        [Key]
        public int Cart_item_id { get; set; }

        [ForeignKey(nameof(Cart))]
        public int Cart_ID { get; set; }

        [ForeignKey(nameof(Product))]
        public int Pro_ID { get; set; }

        public int ?Quantity { get; set; }

        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}