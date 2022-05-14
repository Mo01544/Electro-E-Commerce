using Project_ASP.Net.Models;
using System.Collections.Generic;
using Project_ASP.Net.ViewModel;

namespace Project_ASP.Net.Repositories.Categories
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