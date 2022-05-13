using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Project_ASP.Net.ViewModel

{
    public class CategoryViewModel
    {

        [Required]
        [Remote("isUnique", "Category", ErrorMessage = "the category name is used before")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Picture { get; set; }


    }
}
