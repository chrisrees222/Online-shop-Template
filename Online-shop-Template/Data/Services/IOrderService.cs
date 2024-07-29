using Online_shop_Template.Models;

namespace Online_shop_Template.Data.Services
{
    public interface IOrderService
    {
        Task StoreOrder(List<ShoppingCartItem> items, string userId, string EmalAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
