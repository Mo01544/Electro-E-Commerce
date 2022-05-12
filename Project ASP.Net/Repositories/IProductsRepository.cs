using Microsoft.AspNetCore.Http;
using Project_ASP.Net.Models;
using System.Collections.Generic;

namespace Project_ASP.Net.Repositories
{
    public interface IProductsRepository
    {
        int AddNewProduct(Product NewProduct, IFormFile image);
        int DeleteProduct(int id);
        int EditProduct(int id, Product NewProduct);
        List<Product> GetProducts();
        Product GetProductById(int id);
    }
}