using Microsoft.AspNetCore.Mvc;
using Project_ASP.Net.Models;
using Project_ASP.Net.Repositories.Categories;

namespace Project_ASP.Net.Controllers
{
    public class FilterController : Controller
    {
        private IFilterPanelCategoriesRepository _categoryRepository;

        public FilterController(IFilterPanelCategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index() => View("FilterPanel", new FilterPanelViewModel() { Categories = _categoryRepository.GetAll() });
    }
}
