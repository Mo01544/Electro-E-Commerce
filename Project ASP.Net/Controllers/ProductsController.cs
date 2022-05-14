using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.Net.Models;
using Project_ASP.Net.Repositories.Categories;
using Project_ASP.Net.Repositories;


using System.Collections.Generic;
using X.PagedList;
using Project_ASP.Net.ViewModel;
using System.IO;
using System;

namespace Project_ASP.Net.Controllers
{
    public class ProductsController : Controller
    {
         IProductsRepository ProductsRepository;
        ICategoriesRepository categoryRepository;
        IWebHostEnvironment webHostEnvironment;
        private IFilterPanelCategoriesRepository filterCategoryRepository;

        public ProductsController(IProductsRepository _productsRepository, ICategoriesRepository _categoryRepository, IWebHostEnvironment _webHostEnvironment, IFilterPanelCategoriesRepository _filterCategoryRepository)
        {
            ProductsRepository = _productsRepository;
            categoryRepository = _categoryRepository;
            webHostEnvironment = _webHostEnvironment;
            this.webHostEnvironment = _webHostEnvironment;
            filterCategoryRepository = _filterCategoryRepository;

        }
        


        public IActionResult GetAllProducts(int? page, List<string> filters)
        {
            var pageNumber = page ?? 1;
            int pageSize = 9;

            var OnPageOfProduct = ProductsRepository.GetProducts().ToPagedList(pageNumber, pageSize);
            
            
            List<FilterBrandData> brandDatas = ProductsRepository.GetBrands();
            List<FilterPanalCategoryData> categories = filterCategoryRepository.GetAll();
            return View(new StoreViewModel() { Categories = categories, Brands = brandDatas, Products = OnPageOfProduct, filters = filters });
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
            var CurrentProduct = ProductsRepository.CurrentProducts( ProductName);
            return PartialView("FilteredProducts", CurrentProduct);
        }

        public IActionResult FilterProductByCategory(List<string> categories)
        {
            if (categories.Count > 0)
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
