using Online_shop_Template.Models;
using Microsoft.EntityFrameworkCore;
using Online_shop_Template.Migrations;

namespace Online_shop_Template.Data.Services
{
    public class _ordersService : IOrderService
    {
        private readonly AppDbContext _context;

        public _ordersService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Product).Include(n => n.User).ToListAsync();

            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }

            return orders;
        }        

        

        public async Task StoreOrder(List<ShoppingCartItem> items, string UserId, string userEmailAddress, string checkOutId)
        {
            var order = new Order()
            {
                UserId = UserId,
                Email = userEmailAddress,
                CheckoutId = checkOutId
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    Price = item.Product.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }

    }
}
