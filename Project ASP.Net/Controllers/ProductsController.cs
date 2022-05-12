using Microsoft.AspNetCore.Mvc;
using Project_ASP.Net.Models;
using Project_ASP.Net.Repositories;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Project_ASP.Net.Controllers
{
    public class ProductsController : Controller
    {
 
     
         IProductsRepository ProductsRepository;
        ICategoryRepository categoryRepository;
        IWebHostEnvironment webHostEnvironment;
        public ProductsController(IProductsRepository _productsRepository,ICategoryRepository _categoryRepository, IWebHostEnvironment _webHostEnvironment)
        {
            ProductsRepository = _productsRepository;
            categoryRepository = _categoryRepository;
            this.webHostEnvironment = _webHostEnvironment;

        }


        public IActionResult GetAllProducts()
        {
            List<Product> productList = ProductsRepository.GetProducts();
            return View(productList);
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
            ViewData["CategoryList"] = categoryRepository.GetALLCategories();
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(Product Newproduct,IFormFile image)
        {
            if (ModelState.IsValid == true)
            {
                Newproduct.image = image.ToString();
                ProductsRepository.AddNewProduct(Newproduct,image);
                return RedirectToAction("GetAllProducts");
            }
            else
            {
                ViewData["CategoryList"] = categoryRepository.GetALLCategories();
                return View("AddProduct", Newproduct);
            }
        }



        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            Product oldProduct = ProductsRepository.GetProductById(id);
            if (oldProduct != null)
            {
                ViewData["CategoryList"] = categoryRepository.GetALLCategories();
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
            else
            {
                ViewData["CategoryList"] = categoryRepository.GetALLCategories();
                return View("EditProduct", Newproduct);
            }
        }




    }
}
