using Microsoft.AspNetCore.Mvc;
using Project_ASP.Net.Models;
using Project_ASP.Net.Repositories.Categories;
using Project_ASP.Net.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Project_ASP.Net.Repository;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Project_ASP.Net.Controllers
{
    public class CategoryController : Controller
    {
        ICategoriesRepository cateRepository;
        IWebHostEnvironment webHostEnvironment;

        public CategoryController(ICategoriesRepository catesRepository, IWebHostEnvironment webHostEnvironment)
        {
            cateRepository = catesRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult getCategories()
        {
            return View(cateRepository.GetAll());
        }
        public IActionResult getCategoryById(int id)
        {
            return View(cateRepository.FindById(id));
        }
        public IActionResult CrudCategory(Category cate)
        {

            return View(cateRepository.GetAll());
        }
        public IActionResult AddCategory()
        {
            return View(new Category());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAddCategory(CategoryViewModel cat)
        {

            if (ModelState.IsValid == false)
            {
                return View("AddCategory", cat);
            }

            string categoriesImages = Path.Combine(webHostEnvironment.WebRootPath, "images");
            string UniqueimgName = Guid.NewGuid().ToString() + "_" + cat.Picture.FileName;
            string imgPath = Path.Combine(categoriesImages, UniqueimgName);
            using (var fileStream = new FileStream(imgPath, FileMode.Create))
            {
                cat.Picture.CopyTo(fileStream);
                fileStream.Close();
            }
            this.cateRepository.Insert(new Category() { Name = cat.Name, picture = UniqueimgName, Description = cat.Description });
            return RedirectToAction("CrudCategory");


        }
        public IActionResult EditCategory(int id)
        {
            Category cate = cateRepository.FindById(id);
            if (cate != null)
            {
                return View("EditCategory", cate);
            }
            return RedirectToAction("CrudCategory");
        }
        [HttpPost]
        public IActionResult SaveEditCategory(int id,// [Bind("Name,Address")]
            Category cate)
        {
            if (cate.Name != null)
            {
                cateRepository.Edit(id, cate);
                return RedirectToAction("CrudCategory");
            }
            return View("SaveEditCategory", cate);
        }
        public IActionResult DeleteCategory(int id)
        {
   
            cateRepository.Delete(id);
            return RedirectToAction("CrudCategory");


        }

    }
}
