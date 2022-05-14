using System.Collections.Generic;

namespace Project_ASP.Net.Models
{
    public class StoreViewModel
    {
        public List<FilterPanalCategoryData> Categories { get; set; }
        
        public List<FilterBrandData> Brands { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public List<string> filters { get; set; }
    }
}
