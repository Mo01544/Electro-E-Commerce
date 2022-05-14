using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project_ASP.Net.Models
{
    public class OrderDetails
    {
        public int id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        //[ForeignKey("Order"), Key, Column(Order = 1)]
        [ForeignKey("Order")]

        public int order_id { get; set; }
        public virtual Order Order { get; set; }

        //[ForeignKey("Product"), Key, Column(Order = 2)]
        [ForeignKey("Product")]
        public int product_id { get; set; }
        public virtual Product Product { get; set; }

    }
}
