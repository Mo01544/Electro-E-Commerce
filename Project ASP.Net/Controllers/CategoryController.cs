using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.Net.Models;
using Project_ASP.Net.Repositories.Categories;
using Project_ASP.Net.ViewModel;
using System;
using System.IO;

namespace Project_ASP.Net.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoriesRepository cateRepository;
        private IWebHostEnvironment webHostEnvironment;

        public CategoryController(ICategoriesRepository catesRepository, IWebHostEnvironment webHostEnvironment)
        {
            cateRepository = catesRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult getCategories() => View(cateRepository.GetAll());
        public IActionResult getCategoryById(int id) => View(cateRepository.FindById(id));
        public IActionResult CrudCategory(Category cate) => View(cateRepository.GetAll());
        public IActionResult AddCategory() => View(new Category());
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
            using (FileStream fileStream = new FileStream(imgPath, FileMode.Create))
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
