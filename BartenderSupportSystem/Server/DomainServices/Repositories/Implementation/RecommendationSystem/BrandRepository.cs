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
    public sealed class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IBrandMapper _brandMapper;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
            _brandMapper = new BrandMapper();
        }

        public async Task<IReadOnlyCollection<Brand>> GetAll()
        {
            var brandsList = await _context.BrandsSet.ToListAsync();
            return (from b in brandsList select _brandMapper.DbToDomain(b)).ToList().AsReadOnly();
        }

        public async Task<Brand> GetOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var brandDbModel = await _context.BrandsSet.FindAsync(id);
                return _brandMapper.DbToDomain(brandDbModel);
            }
            else
            {
                return null;
            }
        }

        public async Task<Brand> AddOne(Brand item)
        {
            var exists = await Exists(item.Id);
            if (!exists)
            {
                await _context.BrandsSet.AddAsync(_brandMapper.DomainToDb(item));
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task<Brand> UpdateOne(Brand item)
        {
            var exists = await Exists(item.Id);
            if (exists)
            {
                await Detach(item.Id);
                var enState = _context.BrandsSet.Update(_brandMapper.DomainToDb(item));
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
                var brandDbModel = await _context.BrandsSet.FindAsync(id);
                _context.BrandsSet.Remove(brandDbModel);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.BrandsSet.AnyAsync(brand => brand.Id.Equals(id));
        }

        public async Task Detach(Guid id)
        {
            var entity = await _context.BrandsSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}