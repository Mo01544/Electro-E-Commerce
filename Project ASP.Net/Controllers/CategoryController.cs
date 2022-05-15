using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.Net.Models;
using Project_ASP.Net.Repositories.Categories;
using Project_ASP.Net.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Project_ASP.Net.Controllers
{
    
    public class CategoryController : Controller
    {
        private ICategoriesRepository cateRepository;
        private IWebHostEnvironment webHostEnvironment;
        private ASPContext db;

        public CategoryController(ICategoriesRepository catesRepository, IWebHostEnvironment webHostEnvironment, ASPContext _db)
        {
            db = _db;
            cateRepository = catesRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult getCategories() => View(cateRepository.GetAll());
        public IActionResult getCategoryById(int id) => View(cateRepository.FindById(id));
        public IActionResult CrudCategory(Category cate) => View(cateRepository.GetAll());
        public IActionResult AddCategory() => View(new CategoryViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveAddCategory(CategoryViewModel cat)
        {

            if (ModelState.IsValid == true)
            {

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


            return View("AddCategory", cat);


        }
        public async Task<IActionResult> EditCategory(int id)
        {
          
            var cate = await db.Categories.FindAsync(id);
            var CategoryViewModel = new CategoryViewModel()
            {
                Id = cate.Cat_Id,
                Name = cate.Name,
                Description = cate.Description,
                ExistingImage = cate.picture
            };
            if (cate != null)
            {
                return View(CategoryViewModel);
            }
            return RedirectToAction("CrudCategory");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEditCategory(int id, CategoryViewModel cate)
        {

            if (ModelState.IsValid == true)
            {
                var category = await db.Categories.FindAsync(cate.Id);
                category.Name = cate.Name;
                category.Description = cate.Description;


                if (cate.Picture != null)
                {
                    if (cate.ExistingImage != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images", cate.ExistingImage);
                        System.IO.File.Delete(filePath);
                    }

                    category.picture = ProcessUploadedFile(cate);
                }
                db.Update(category);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(CrudCategory));
            }

            else
            {
                    return View("EditCategory", cate);
            }
        } 
        public IActionResult DeleteCategory(int id)
        {

            cateRepository.Delete(id);
            return RedirectToAction("CrudCategory");


        }
        private string ProcessUploadedFile(CategoryViewModel cate)
        {
            string uniqueFileName = null;

            if (cate.Picture != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + cate.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    cate.Picture.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

    }
}
