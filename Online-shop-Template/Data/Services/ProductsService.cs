using Online_shop_Template.Data.Base;
using Online_shop_Template.Data.ViewModels;
using Online_shop_Template.Models;
using Microsoft.EntityFrameworkCore;

namespace Online_shop_Template.Data.Services
{
    public class ProductsService: EntityBaseRepository<Product>, IProductService
    {
        private readonly AppDbContext _context;

        public ProductsService(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task AddNewProductAsync(NewProductVM data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            

        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetails = await _context.Products
            
            .FirstOrDefaultAsync(n=>n.Id == id);

            return productDetails;
        }

        public async Task<NewProductDropdownsVM> GetNewProductDropdownsVMValues()
        {
            var response = new NewProductDropdownsVM()
            {
                //Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                //Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                //Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;

        }

        public async Task<NewProductDropdownsVM> GetNewProductDropdownsValues()
        {
            var response = new NewProductDropdownsVM()
            {
                //Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                //Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                //Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateProductAsync(NewProductVM data)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbProduct != null)
            {
                dbProduct.Name = data.Name;
                dbProduct.Description = data.Description;
                dbProduct.Price = data.Price;
                dbProduct.ImageURL = data.ImageURL;
                dbProduct.StartDate = data.StartDate;
                dbProduct.EndDate = data.EndDate;
                await _context.SaveChangesAsync();
            }
                        
            await _context.SaveChangesAsync();
        }
    }
}
