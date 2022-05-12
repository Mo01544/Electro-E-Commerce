using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project_ASP.Net.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project_ASP.Net.Repositories
{

    public class ProductsRepository : IProductsRepository
    {
        ASPContext aSPContext;
        IWebHostEnvironment webHostEnvironment;

        public ProductsRepository(ASPContext _aSPContext, IWebHostEnvironment _webHostEnvironment)
        {
            aSPContext = _aSPContext;
            this.webHostEnvironment = _webHostEnvironment;
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
                oldProduct.Product_Brand= NewProduct.Product_Brand;


                return aSPContext.SaveChanges();
            }

            else
            {
                return 0;
            }
        }
        public int AddNewProduct(Product NewProduct,IFormFile image)
        {
            string uploadfolder = Path.Combine(webHostEnvironment.WebRootPath, "image");
            string uniquefilename=Guid.NewGuid().ToString()+"_"+image.FileName;
            string filepath=Path.Combine(uploadfolder, uniquefilename);
            using (var fileStream = new FileStream(filepath, FileMode.Create))
			{
                image.CopyTo(fileStream);
                fileStream.Close();
			}
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
