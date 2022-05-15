using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace Project_ASP.Net.ViewModel
{
    public class UplodeImageViewModel
    {
        
        public IFormFile Picture { get; set; }
    }
}
