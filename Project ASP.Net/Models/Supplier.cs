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
    public class Supplier
    {
        [Key]
        public int Supplier_Id { get; set; }
        [Range(minimum: 5, maximum: 30, ErrorMessage = "Supplier Name Must Between 5,30")]
        public string Supplier_Name { get; set; }
        [Range(minimum: 5, maximum: 30, ErrorMessage = "Supplier Title Must Between 5,30")]
        public string Supplier_Title { get; set; }
    }
}
