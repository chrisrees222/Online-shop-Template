using Online_shop_Template.Data.Cart;
using Online_shop_Template.Data.Services;
using Online_shop_Template.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Online_shop_Template.Models;
using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Org.BouncyCastle.Asn1.Cms;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Online_shop_Template.Migrations;


namespace Online_shop_Template.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IProductService _productService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;
        private readonly StripeSettings _stripeSettings;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(IConfiguration configuration, UserManager<ApplicationUser> userManager, IProductService productsService, ShoppingCart shoppingCart, IOrderService ordersService, IOptions<StripeSettings> stripeSettings)
        {
            _configuration = configuration;
            _productService = productsService;
            _shoppingCart = shoppingCart;
            _orderService = ordersService;
            _stripeSettings = stripeSettings.Value;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _orderService.GetOrdersByUserIdAndRoleAsync(userId, userRole);

            return View(orders);
        }

        public IActionResult CreateCheckoutSession(float amount)
        {
            var currency = "gbp"; // Currency code
            var successUrl = "https://localhost:7027/Orders/Index";
            var cancelUrl = "https://localhost:7027/Orders/ShoppingCart";
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            UnitAmount = Convert.ToInt32(amount) * 100,  // Amount in smallest currency unit (e.g., cents)
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Product Name",
                                Description = "Product Description"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                Metadata = new Dictionary<string, string>
                {
                    { "UserId", userId.ToString() },
                    { "UserRole", userRole.ToString() },
                    
                }
            };
            var service = new SessionService();
            var session = service.Create(options);

            CompleteOrder(session.Id).GetAwaiter().GetResult(); 

            return Redirect(session.Url);
        }
        public async Task<IActionResult> Success()
        {
            return View("OrderCompleted");
        }
        public IActionResult Cancel()
        {
            return View("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _productService.GetProductByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _productService.GetProductByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        
        public async Task<IActionResult> CompleteOrder(string checkOutId)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);            
            
            await _orderService.StoreOrder(items, userId, userEmailAddress, checkOutId);
             
            await _shoppingCart.ClearShoppingCartAsync();    

            return View("OrderCompleted");
        }



    }
}
