using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.DomainServices.Mappers.Implementation.RecommendationSystem;
using BartenderSupportSystem.Server.DomainServices.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Server.DomainServices.Repositories.Interfaces.RecommendationSystem;
using Microsoft.EntityFrameworkCore;

namespace BartenderSupportSystem.Server.DomainServices.Repositories.Implementation.RecommendationSystem
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductMapper _productMapper;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
            _productMapper = new ProductMapper();
        }

        public async Task<IReadOnlyCollection<Product>> GetAll()
        {
            var productsList = await _context.ProductsSet.ToListAsync();
            return (from p in productsList select _productMapper.DbToDomain(p)).ToList().AsReadOnly();
        }

        public async Task<Product> GetOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var productDbModel = await _context.ProductsSet.FindAsync(id);
                return _productMapper.DbToDomain(productDbModel);
            }
            else
            {
                return null;
            }
        }

        public async Task<Product> AddOne(Product item)
        {
            var exists = await Exists(item.Id);
            if (!exists)
            {
                await _context.ProductsSet.AddAsync(_productMapper.DomainToDb(item));
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task<Product> UpdateOne(Product item)
        {
            var exists = await Exists(item.Id);
            if (exists)
            {
                await Detach(item.Id);
                var enState = _context.ProductsSet.Update(_productMapper.DomainToDb(item));
                enState.State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var productDbModel = await _context.ProductsSet.FindAsync(id);
                _context.ProductsSet.Remove(productDbModel);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.ProductsSet.AnyAsync(product => product.Id.Equals(id));
        }

        public async Task Detach(Guid id)
        {
            var entity = await _context.ProductsSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
