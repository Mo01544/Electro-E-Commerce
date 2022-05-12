using System.Collections.Generic;

namespace Project_ASP.Net.Repositories
{
    public interface IRepository<T>
    {
        int Insert(T entity);
        List<T> GetAll();
        T GetById(int id);
        int Update(T entity);
        int Delete(T entity);

    }
}
