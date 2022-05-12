using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project_ASP.Net.Models
{
    public class OrderDetails
    {

        [ForeignKey("Order"), Key, Column(Order = 1)]
        public int order_id { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey("Product"), Key, Column(Order = 2)]
        public int product_id { get; set; }
        public virtual Product Product { get; set; }

    }
}
