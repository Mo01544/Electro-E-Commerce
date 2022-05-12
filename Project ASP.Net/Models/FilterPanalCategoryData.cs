using Microsoft.EntityFrameworkCore;

namespace Project_ASP.Net.Models
{
    [Keyless]
    public class FilterPanalCategoryData
    {
        public int NumberOfProducts { get; set; }
        public string CategoryName { get; set; }
    }
}
