using System.Collections.Generic;
using Project_ASP.Net.Models;
namespace Project_ASP.Net.Repository
{
   
        public interface ICategoriesRepository
        {
        int Delete(int id);
        int Edit(int id, Category cate);
        Category FindById(int id);
        List<Category> GetAll();
        int Insert(Category d);

        }
    
}
