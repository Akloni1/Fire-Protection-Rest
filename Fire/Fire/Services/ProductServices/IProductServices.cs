using Fire.ViewModels.Product;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fire.Services.ProductServices
{
    public interface IProductServices
    {
        Task<ProductViewModels> GetProduct(int id);
        Task<ICollection<ProductViewModels>> GetAllProducts();
        Task<ProductViewModels> UpdateProduct(int id, EditProductViewModels productModel);
        Task<ProductViewModels> AddProduct(InputProductViewModels viewModel);
        Task<ProductViewModels> DeleteProduct(int id);
        Task<decimal?> GetCostById(int id, decimal km);
    }
}
