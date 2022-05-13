using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.Net.Models;
using Project_ASP.Net.Repositories;
using Project_ASP.Net.Repositories.Categories;
using System.Collections.Generic;

namespace Project_ASP.Net.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsRepository ProductsRepository;
        private ICategoryRepository categoryRepository;
        private IWebHostEnvironment webHostEnvironment;
        private IFilterPanelCategoriesRepository filterCategoryRepository;

        public ProductsController(IProductsRepository _productsRepository, ICategoryRepository _categoryRepository, IWebHostEnvironment _webHostEnvironment, IFilterPanelCategoriesRepository _filterCategoryRepository)
        {
            ProductsRepository = _productsRepository;
            categoryRepository = _categoryRepository;
            this.webHostEnvironment = _webHostEnvironment;
            filterCategoryRepository = _filterCategoryRepository;

        }


        public IActionResult GetAllProducts(List<string> filters)
        {
            List<Product> productList = ProductsRepository.GetProducts();
            List<FilterBrandData> brandDatas = ProductsRepository.GetBrands();
            List<FilterPanalCategoryData> categories = filterCategoryRepository.GetAll();
            return View(new StoreViewModel() { Categories = categories, Brands = brandDatas, Products = productList, filters = filters });
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
        public IActionResult AddProduct(Product Newproduct, IFormFile image)
        {
            if (ModelState.IsValid == true)
            {
                Newproduct.image = image.ToString();
                ProductsRepository.AddNewProduct(Newproduct, image);
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

        public IActionResult FilterProductByCategory(List<string> categories)
        {
            if(categories.Count > 0)
            {
            List<Product> filterProducts = ProductsRepository.FilterByCategory(categories);
            return PartialView("FilteredProducts", filterProducts);

            }
            else
            {
                List<Product> allProducts = ProductsRepository.GetProducts();
                return PartialView("FilteredProducts", allProducts);
            }
        }

        public IActionResult FilterCategoryByBrand(List<string> brands)
        {
            if (brands.Count > 0)
            {
                List<Product> filterProducts = ProductsRepository.FilterByBrand(brands);
                return PartialView("FilteredProducts", filterProducts);

            }
            else
            {
                List<Product> allProducts = ProductsRepository.GetProducts();
                return PartialView("FilteredProducts", allProducts);
            }
        }


    }
}
