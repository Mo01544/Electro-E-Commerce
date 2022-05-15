using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_ASP.Net.Models;
namespace Project_ASP.Net.ViewModel

{
    public class CategoryViewModel:EditImageViewModel
    {

        [Required(ErrorMessage = "Category Name Is Requierd")]
        [MinLength(length: 3, ErrorMessage = "must be 3 letter or more")]
        [MaxLength(length: 25, ErrorMessage = "must be less than 25 letter")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description Is Requierd")]
        [MinLength(length: 20, ErrorMessage = "must be 20 letter or more")]
        [MaxLength(length: 100, ErrorMessage = "must be less than 100 letter")]
        public string Description { get; set; }




    }
}
