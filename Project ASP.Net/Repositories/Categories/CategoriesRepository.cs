﻿using Project_ASP.Net.Models;
using System.Collections.Generic;
using System.Linq;
namespace Project_ASP.Net.Repositories.Categories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private ASPContext db;

        public CategoriesRepository(ASPContext _db)
        {
            db = _db;
        }

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

        public Category FindById(int id) => db.Categories.FirstOrDefault(x => x.Cat_Id == id);

        public List<Category> GetAll() => db.Categories.ToList();

        public int Insert(Category cate)
        {
            db.Categories.Add(cate);
            int saveAddCategory = db.SaveChanges();
            return saveAddCategory;
        }
    }
}