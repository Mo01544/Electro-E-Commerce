using Microsoft.AspNetCore.Mvc;
using Project_ASP.Net.Models;
using Project_ASP.Net.Repositories.Categories;
using Project_ASP.Net.Repositories;

using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using Project_ASP.Net.ViewModel;
using X.PagedList;


namespace Project_ASP.Net.Controllers
{
    public class ProductsController : Controller
    {
         IProductsRepository ProductsRepository;
        ICategoriesRepository categoryRepository;
        IWebHostEnvironment webHostEnvironment;
        public ProductsController(IProductsRepository _productsRepository, ICategoriesRepository _categoryRepository, IWebHostEnvironment _webHostEnvironment)
        {
            ProductsRepository = _productsRepository;
            categoryRepository = _categoryRepository;
            webHostEnvironment = _webHostEnvironment;
        }
        public IActionResult GetAllProducts(int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 9;

            var OnPageOfProduct = ProductsRepository.GetProducts().ToPagedList(pageNumber, pageSize);
            return View(OnPageOfProduct);
        }


        public IActionResult Details(int id)
        {
            Product product = ProductsRepository.GetProductById(id);
            return View(product);
        }
        public IActionResult Delete(int id)
        {
            ProductsRepository.DeleteProduct(id);
            return RedirectToAction("GetAllProducts");
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewData["CategoryList"] = categoryRepository.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(ProductViewModel Newproduct)
        { 
            if (ModelState.IsValid == true)

            {
                string uploadfolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
                string uniquefilename = Guid.NewGuid().ToString() + "_" + Newproduct.image.FileName;
                string filepath = Path.Combine(uploadfolder, uniquefilename);
                using (var fileStream = new FileStream(filepath, FileMode.Create))
                {
                    Newproduct.image.CopyTo(fileStream);
                    fileStream.Close();
                }
                this.ProductsRepository.AddNewProduct(new Product()
                {
                    image = uniquefilename,
                    Unit_Price = Newproduct.Unit_Price,
                    Pro_Name = Newproduct.Name,
                    Description = Newproduct.Description,
                    Stock = Newproduct.Stock,
                    Product_Brand = Newproduct.Brand,
                    Discount = Newproduct.Discount,
                    cat_id = Newproduct.Cat_id
                }) ;
                return RedirectToAction("GetAllProducts", Newproduct);
            }
            ViewData["CategoryList"] = categoryRepository.GetAll();
                return View("AddProduct", Newproduct);   
        }
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            Product oldProduct = ProductsRepository.GetProductById(id);
            if (oldProduct != null)
            {

                ViewData["CategoryList"] = categoryRepository.GetAll();
                return View("EditProduct", oldProduct);
            }
            return RedirectToAction("GetAllProducts");
        }
        [HttpPost]
        public IActionResult EditProduct(int id, Product Newproduct)
        {
            if (ModelState.IsValid == true)
            {
                ProductsRepository.EditProduct(id, Newproduct);
                return RedirectToAction("GetAllProducts");
            }
            
                ViewData["CategoryList"] = categoryRepository.GetAll();
                return View("EditProduct", Newproduct);     
        }
         //search
        public IActionResult Search(string ProductName)
        {
            var CurrentProduct = ProductsRepository.CurrentProducts(ProductName);
            return View("GetAllProducts", CurrentProduct);
        }
    }
}
