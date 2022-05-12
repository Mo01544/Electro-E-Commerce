using Project_ASP.Net.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project_ASP.Net.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        ASPContext aspcontext;
        public CategoryRepository(ASPContext _aSPContext)
        {
            aspcontext = _aSPContext;
        }

        public List<Category> GetALLCategories()
        {
            return aspcontext.Categories.ToList();
        }

        public Category FindCategoryByID(int _CategoryId)
        {
            return aspcontext.Categories.FirstOrDefault(c => c.Cat_Id == _CategoryId);
        }

        public int AddNewCategory(Category NewCategory)
        {
            aspcontext.Categories.Add(NewCategory);
            return aspcontext.SaveChanges();
        }

        public int DeleteCategory(int _CategoryId)
        {
            Category category = FindCategoryByID(_CategoryId);
            aspcontext.Categories.Remove(category);
            return aspcontext.SaveChanges();
        }

        public int EditCategory(int _CategoryId, Category NewCategory)
        {
            Category oldCategory = FindCategoryByID(_CategoryId);
            if (oldCategory != null)
            {
                oldCategory.Description = NewCategory.Description;
                oldCategory.Name = NewCategory.Name;
                oldCategory.picture = NewCategory.picture;
                return aspcontext.SaveChanges();
            }
            return 0;

        }



    }
}
