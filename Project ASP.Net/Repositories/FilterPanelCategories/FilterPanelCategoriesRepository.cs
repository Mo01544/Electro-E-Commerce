using Project_ASP.Net.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project_ASP.Net.Repositories.Categories
{
    public class FilterPanelCategoriesRepository : IFilterPanelCategoriesRepository
    {
        private readonly ASPContext _db;

        public FilterPanelCategoriesRepository(ASPContext db)
        {
            _db = db;
        }

        public List<FilterPanalCategoryData> GetAll() => _db.Products.GroupBy(x => x.Category.Name).Select(x => new FilterPanalCategoryData { CategoryName = x.Key, NumberOfProducts = x.Count() }).ToList();
    }
}