using Project_ASP.Net.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_ASP.Net.Repositories
{
    public interface IOrderservices
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string Userid, string UserEmailAddress);
        Task<List<Order>> GetOrderByUserAsync(string Userid);
    }
}
