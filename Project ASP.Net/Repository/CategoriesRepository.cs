using System.Collections.Generic;
using Project_ASP.Net.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace Project_ASP.Net.Repository
{
    public class CategoriesRepository : ICategoriesRepository
    {
        ASPContext db = new ASPContext();

        public int Delete(int id)
        {
            Category oldCategory = FindById(id);
            db.Categories.Remove(oldCategory);
            return db.SaveChanges();
        }

        public int Edit(int id, Category cate)
        {
            Category oldCategory = FindById(id);
            if (oldCategory != null)
            {
                oldCategory.Name = cate.Name;
                oldCategory.Description = cate.Description;
                oldCategory.picture = cate.picture;
                int saveUpdatedCategory = db.SaveChanges();
                return saveUpdatedCategory;
            }
            return 0;
        }

        public Category FindById(int id)
        {
            return db.Categories.FirstOrDefault(x => x.Cat_Id == id);
        }

        public List<Category> GetAll()
        {
            return db.Categories.ToList();
        }

        public int Insert(Category cate)
        {
            db.Categories.Add(cate);
            int saveAddCategory = db.SaveChanges();
            return saveAddCategory;
        }
    }
}
