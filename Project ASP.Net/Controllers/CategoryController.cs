using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_ASP.Net.Models;
using Project_ASP.Net.ViewModel;
using System.IO;
using Project_ASP.Net.Repository;
using System;

namespace Project_ASP.Net.Controllers
{
    public class CategoryController : Controller
    {
        ICategoriesRepository cateRepository;

        public CategoryController(ICategoriesRepository catesRepository)
        {
            cateRepository = catesRepository;
        }

        public IActionResult getCategories()
        {
            return View(cateRepository.GetAll());
        }
        public IActionResult getCategoryById(int id)
        {
            return View(cateRepository.FindById(id));
        }
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
