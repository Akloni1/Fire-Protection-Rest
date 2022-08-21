using AutoMapper;
using Fire.Data;
using Fire.Models;
using Fire.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fire.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly eeContext _context;
        private readonly IMapper _mapper;

        public ProductServices(IMapper mapper, eeContext context)
        { 
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProductViewModels> AddProduct(InputProductViewModels viewModel)
        {
            var productModel = _mapper.Map<Product>(viewModel);
            var product = _context.Add(productModel).Entity;
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductViewModels>(product);
        }

        public async Task<ProductViewModels> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return null;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductViewModels>(product);
        }

        public async Task<ICollection<ProductViewModels>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return _mapper.Map<ICollection<Product>, ICollection<ProductViewModels>>(products);
        }

        public async Task<ProductViewModels> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(a=>a.IdProduct == id);
            return _mapper.Map<ProductViewModels>(product);
        }

        public async Task<decimal?> GetCostById(int id, decimal km)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(a => a.IdProduct == id);

                var res = product.Price * km;
                return res;
            }
            catch (NullReferenceException ex)
            {
                return 0;
            }
        }

        public async Task<ProductViewModels> UpdateProduct(int id, EditProductViewModels productModel)
        {
            try
            {

                Product product = _mapper.Map<Product>(productModel);
                product.IdProduct = id;
                _context.Update(product);
                await _context.SaveChangesAsync();
                return _mapper.Map<ProductViewModels>(product);
            }
            catch (DbUpdateException)
            {
                if (!await ProductExists(id))
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(e => e.IdProduct== id);
        }

    }
}
