using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Project_ASP.Net.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }
        [Key]
        public int Cat_Id { get; set; }
        [Required(ErrorMessage = "Category Name Is Requierd")]
        [MinLength(length: 3, ErrorMessage = "must be 3 letter or more")]
        [MaxLength(length: 25, ErrorMessage = "must be less than 25 letter")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description Is Requierd")]
        [MinLength(length: 7, ErrorMessage = "must be 7 letter or more")]
        [MaxLength(length: 100, ErrorMessage = "must be less than 100 letter")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Picture Is Requierd")]
        public string picture { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
