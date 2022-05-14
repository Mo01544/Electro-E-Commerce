using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Project_ASP.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_ASP.Net.Repositories
{
    public class ShoppingCart
    {
        public ASPContext aSPContext { get; set; }
        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(ASPContext _aSPContext)
        {

            aSPContext = _aSPContext;
        }
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()
                ?.HttpContext.Session;
            var context = services.GetService<ASPContext>();
            string Cartid = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", Cartid);
            return new ShoppingCart(context) { ShoppingCartId = Cartid };
        }
        public void AddItemtoCart(Product product)
        {
            var Shoppingcartitem = aSPContext.ShoppingCartItems.FirstOrDefault(n => n.product.Pro_Id == product.Pro_Id &&
              n.ShoppingCartId == ShoppingCartId);
            if (Shoppingcartitem == null)
            {
                Shoppingcartitem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    product = product,
                    Amount = 1
                };
                aSPContext.ShoppingCartItems.Add(Shoppingcartitem);

            }
            else
            {
                Shoppingcartitem.Amount++;
            }
            aSPContext.SaveChanges();
        }

        public void Removeitemfromcart(Product product)
        {
            var Shoppingcartitem = aSPContext.ShoppingCartItems.FirstOrDefault(n => n.product.Pro_Id == product.Pro_Id &&
             n.ShoppingCartId == ShoppingCartId);
            if (Shoppingcartitem != null)
            {
                if (Shoppingcartitem.Amount > 1)
                {
                    Shoppingcartitem.Amount--;
                }
                else
                {
                    aSPContext.ShoppingCartItems.Remove(Shoppingcartitem);

                }

            }

            aSPContext.SaveChanges();
        }
        public List<ShoppingCartItem> CetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = aSPContext.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.product).ToList());
        }


        public double GetShoppingCartTotal()
        {
            var total = aSPContext.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n =>
             n.product.Unit_Price * n.Amount).Sum();
            return total;
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await aSPContext.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            aSPContext.ShoppingCartItems.RemoveRange(items);
            await aSPContext.SaveChangesAsync();

        }
    }
}
