 using Online_shop_Template.Data.Base;
using Online_shop_Template.Data.ViewModels;
using Online_shop_Template.Models;

namespace Online_shop_Template.Data.Services
{
    public interface IProductService: IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);

        Task<NewProductDropdownsVM> GetNewProductDropdownsVMValues();

        Task AddNewProductAsync(NewProductVM data);

        Task UpdateProductAsync(NewProductVM product);
    }
}
