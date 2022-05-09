using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Project_ASP.Net.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCart_Id { get; set; }
        public int NumOfProduct { get; set; }
        public decimal Total_Price { get; set; }
    }
}
