using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_ASP.Net.ViewModel
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Product Name Is Requierd")]
        //[Range(minimum: 5, maximum: 30, ErrorMessage = "Product Name Must Between 5,30")]
        [MinLength(length: 3, ErrorMessage = "Product Name Must More Than 3 char")]
        [MaxLength(300, ErrorMessage = "Product Name Must Less Than 30 char")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description Is Requierd")]
        [MinLength(20, ErrorMessage = "Product Name Must More Than 20 char")]
        [MaxLength(300, ErrorMessage = "Product Name Must Less Than 100 char")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Product_Brand Is Requierd")]
        [MinLength(length: 3, ErrorMessage = "Product_Brand Must More Than 3 char")]
        [MaxLength(30, ErrorMessage = "Product_Brand Must Less Than 30 char")]
        public string Brand { get; set; }
        [Column(TypeName = "money")]
        public double Unit_Price { get; set; }
        public int Stock { get; set; }
        [Required(ErrorMessage = "Picture Is Requierd")]
        public IFormFile image { get; set; }
        [Column(TypeName = "money")]
        public int Discount { get; set; }
        public int Cat_id { get; set; }

    }
}
