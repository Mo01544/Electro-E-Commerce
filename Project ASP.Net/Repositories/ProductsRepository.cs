using Microsoft.EntityFrameworkCore;
using Project_ASP.Net.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project_ASP.Net.Repositories
{

    public class ProductsRepository : IProductsRepository
    {
        private ASPContext aSPContext;
        public ProductsRepository(ASPContext _aSPContext)
        {
            aSPContext = _aSPContext;
        }

        public List<Product> GetProducts() => aSPContext.Products.ToList();

        public Product GetProductById(int id) => aSPContext.Products.Include(c => c.Category).FirstOrDefault(p => p.Pro_Id == id);
        public int EditProduct(int id, Product NewProduct)
        {
            Product oldProduct = GetProductById(id);
            if (oldProduct != null)
            {
                oldProduct.Pro_Name = NewProduct.Pro_Name;
                oldProduct.Unit_Price = NewProduct.Unit_Price;
                oldProduct.Description = NewProduct.Description;
                oldProduct.NumSeller = NewProduct.NumSeller;
                oldProduct.image = NewProduct.image;
                oldProduct.Discount = NewProduct.Discount;
                oldProduct.Stock = NewProduct.Stock;


                return aSPContext.SaveChanges();
            }

            else
            {
                return 0;
            }
        }
        public int AddNewProduct(Product NewProduct)
        {
            aSPContext.Products.Add(NewProduct);
            return aSPContext.SaveChanges();
        }
        public int DeleteProduct(int id)
        {
            Product oldProduct = GetProductById(id);
            if (oldProduct != null)
            {
                aSPContext.Products.Remove(oldProduct);
                return aSPContext.SaveChanges();
            }

            else
            {
                return 0;
            }
        }








    }
}
