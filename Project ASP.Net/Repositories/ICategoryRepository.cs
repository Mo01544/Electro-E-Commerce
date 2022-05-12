using Project_ASP.Net.Models;
using System.Collections.Generic;

namespace Project_ASP.Net.Repositories
{
    public interface ICategoryRepository
    {
        int AddNewCategory(Category NewCategory);
        int DeleteCategory(int _CategoryId);
        int EditCategory(int _CategoryId, Category NewCategory);
        Category FindCategoryByID(int _CategoryId);
        List<Category> GetALLCategories();
    }
}