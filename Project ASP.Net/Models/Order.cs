using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ASP.Net.Models
{
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime Date { get; set; }
        //[Required(ErrorMessage = " product_Quantity Is Requierd")]
        //public int product_Quantity { get; set; }
        //public string Status { get; set; }
        //public int Total_price { get; set; }
        //[ForeignKey("product_id")]
        //public int product_id { get; set; }
        //public virtual Product Product { get; set; }
        public string Email{ get; set; }
        [ForeignKey("applicationUser")]
        public string user_id { get; set; }
        public virtual ApplicationUser applicationUser { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
