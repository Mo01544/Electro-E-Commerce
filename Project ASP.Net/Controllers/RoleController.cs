using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.Net.ViewModel;
using System.Threading.Tasks;

namespace Project_ASP.Net.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(RoleViewModel NewRole)
        {
            if(ModelState.IsValid==true)
            {
                IdentityRole  role = new IdentityRole();
                role.Name = NewRole.RoleName;
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded==true)
                {
                    return View(new RoleViewModel());
                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("",item.Description);
                    }
                }
            }
            return View(NewRole);
        }
    }
}
