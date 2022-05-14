using Microsoft.EntityFrameworkCore;
using Project_ASP.Net.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_ASP.Net.Repositories
{
    public class OrderService : IOrderservices
    {
        private readonly ASPContext _aSPContext;

        public OrderService(ASPContext aSPContext)
        {
            _aSPContext = aSPContext;
        }
        public async Task<List<Order>> GetOrderByUserAsync(string Userid)
        {
            var orders =await _aSPContext.Orders.Include(n=>n.OrderDetails).ThenInclude(n=>n.Product).Where(n=>n.user_id == Userid).ToListAsync();
            return orders;
        }


        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string Userid, string UserEmailAddress)
        {
            var order = new Order()
            {
                user_id = Userid,
                Email = UserEmailAddress,

            };
            await _aSPContext.Orders.AddAsync(order);
            await _aSPContext.SaveChangesAsync();
            foreach (var item in items)
            {
                var orderitem = new OrderDetails()
                {
                    Amount = item.Amount,
                    product_id = item.product.Pro_Id,
                    order_id = order.Order_Id,
                    Price = item.product.Unit_Price
                };
                await _aSPContext.OrderDetails.AddAsync(orderitem);
            }
            await _aSPContext.SaveChangesAsync();
        }
    }
}
