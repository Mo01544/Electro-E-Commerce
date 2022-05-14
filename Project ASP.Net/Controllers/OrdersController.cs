using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_ASP.Net.Repositories;
using Project_ASP.Net.ViewModel;
using System.Threading.Tasks;

using System.Security.Claims;
using Project_ASP.Net.Models;

namespace Project_ASP.Net.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderservices orderservices;

        private readonly UserManager<ApplicationUser> userManger;


        public OrdersController(IProductsRepository productsRepository,ShoppingCart shoppingCart
           ,IOrderservices _orderservices, UserManager<ApplicationUser> _userManager)
        {
            _productsRepository = productsRepository;
            _shoppingCart = shoppingCart;
            this.orderservices = _orderservices;
            userManger = _userManager;
        }

        public async Task<IActionResult> OrderList()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Orders =await orderservices.GetOrderByUserAsync(userid);
            return View(Orders);
        }
        public IActionResult shoppingCart()
        {
            var items = _shoppingCart.CetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
            };
            return View(response);
        }


        public IActionResult AddtoShoppingCart(int id)
        {
            var item = _productsRepository.GetProductById(id);
            if(item != null)
            {
                _shoppingCart.AddItemtoCart(item);
            }
            return RedirectToAction(nameof(shoppingCart));
        }
        public IActionResult removefromShoppingCart(int id)
        {
            var item = _productsRepository.GetProductById(id);
            if (item != null)
            {
                _shoppingCart.Removeitemfromcart(item);
            }
            return RedirectToAction(nameof(shoppingCart));
        }


        public async  Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.CetShoppingCartItems();
            //var userid = await userManager.FindByEmailAsync(User.Identity.Name);
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
                //var userEmailAddress = user.Email; 
            await orderservices.StoreOrderAsync(items, userid, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }

    }
}
