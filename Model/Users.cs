using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Model
{
   
    internal class Users
    {
        [Key]
        public int User_id { get; set; }
        [MaxLength(250)]
        [Required]

        public string? UserName { get; set; }
        [MaxLength(250)]
        [Required]

        public string? Password { get; set; }
        [MaxLength(250)]
        public string? Role { get; set; }
        public ICollection<Cart> ?Carts { get; set; }

    }
}
