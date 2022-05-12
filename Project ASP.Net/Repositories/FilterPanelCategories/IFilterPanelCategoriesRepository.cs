using Project_ASP.Net.Models;
using System.Collections.Generic;

namespace Project_ASP.Net.Repositories.Categories
{
    public interface IFilterPanelCategoriesRepository
    {
        List<FilterPanalCategoryData> GetAll();
    }
}
