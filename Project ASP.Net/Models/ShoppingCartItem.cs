using System.ComponentModel.DataAnnotations;

namespace Project_ASP.Net.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int id { get; set; }

        public Product product { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
