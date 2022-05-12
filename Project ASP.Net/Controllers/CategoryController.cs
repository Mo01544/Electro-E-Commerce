using Microsoft.AspNetCore.Mvc;
using Project_ASP.Net.Models;
using Project_ASP.Net.Repositories.Categories;
using Project_ASP.Net.ViewModel;

namespace Project_ASP.Net.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoriesRepository cateRepository;

        public CategoryController(ICategoriesRepository catesRepository)
        {
            cateRepository = catesRepository;
        }

        public IActionResult getCategories() => View(cateRepository.GetAll());
        public IActionResult getCategoryById(int id) => View(cateRepository.FindById(id));
        public IActionResult AddCategory(Category cate)
        {
            return View(
                    new CategoryViewModel()
                    {
                        cate = new Category()
                    }
                );
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCategory(Category cate)
        {
            if (ModelState.IsValid == true)
            {
                cateRepository.Insert(cate);
                return RedirectToAction("getCategories");
            }
            else
            {
                return View("AddCategory", cate);
            }

        }


    }
}
