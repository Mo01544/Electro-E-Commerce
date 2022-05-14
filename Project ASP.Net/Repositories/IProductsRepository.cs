using Microsoft.AspNetCore.Http;
using Project_ASP.Net.Models;
using System.Collections.Generic;

namespace Project_ASP.Net.Repositories
{
    public interface IProductsRepository
    {
        int AddNewProduct(Product NewProduct);
        int DeleteProduct(int id);
        int EditProduct(int id, Product NewProduct);
        List<Product> GetProducts();
        Product GetProductById(int id);
        List<FilterBrandData> GetBrands();
        List<Product> FilterByCategory(List<string> categories);
        List<Product> FilterByBrand(List<string> brands);
        //search
        List<Product> CurrentProducts(string ProductName);
    }
}