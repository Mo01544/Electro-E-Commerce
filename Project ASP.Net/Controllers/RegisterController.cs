using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.Net.Models;
using Project_ASP.Net.ViewModel;
using System.Threading.Tasks;

namespace Project_ASP.Net.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> userManger;
        private readonly SignInManager<ApplicationUser> signInManager;

        public RegisterController(UserManager<ApplicationUser> _UserManger,
            SignInManager<ApplicationUser> _signInManager)
        {
            userManger = _UserManger;
            signInManager = _signInManager;
        }
        [HttpGet]
        public IActionResult Register() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Register(RegisterViewModel RegisterUserVm)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                
                applicationUser.UserName = RegisterUserVm.UserName;
                applicationUser.Email = RegisterUserVm.Email;
                applicationUser.PasswordHash = RegisterUserVm.Password;
                IdentityResult result = await userManger.CreateAsync(applicationUser, RegisterUserVm.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(applicationUser, false);
                    await userManger.AddToRoleAsync(applicationUser, "User");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(RegisterUserVm);
        }
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginvm)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser loginmodel = await userManger.FindByEmailAsync(loginvm.Email);
                if (loginmodel != null)
                {
                    bool found = await userManger.CheckPasswordAsync(loginmodel, loginvm.Password);
                    if (found == true)
                    {

                        await signInManager.SignInAsync(loginmodel, loginvm.RememberMe);

                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Email & Password Not Correct");

            }
            return View(loginvm);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdmin(RegisterViewModel adminvm)
        {
            if (ModelState.IsValid == true)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = adminvm.UserName;
                applicationUser.Email = adminvm.Email;
                applicationUser.PasswordHash = adminvm.Password;
                IdentityResult result = await userManger.CreateAsync(applicationUser, adminvm.Password);
                if (result.Succeeded)
                {
                    await userManger.AddToRoleAsync(applicationUser, "Admin");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(adminvm);
        }
    }
}
