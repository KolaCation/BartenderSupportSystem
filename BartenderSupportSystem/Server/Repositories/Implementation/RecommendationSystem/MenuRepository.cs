using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.CustomDbContextFolder;
using BartenderSupportSystem.Server.Mappers.Implementation.RecommendationSystem;
using BartenderSupportSystem.Server.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Server.Repositories.Interfaces.RecommendationSystem;
using Microsoft.EntityFrameworkCore;

namespace BartenderSupportSystem.Server.Repositories.Implementation.RecommendationSystem
{
    public sealed class MenuRepository : IMenuRepository
    {
        private readonly CustomDbContext _context;
        private readonly IMenuMapper _menuMapper;

        public MenuRepository(CustomDbContext context)
        {
            _context = context;
            _menuMapper = new MenuMapper();
        }

        public async Task<IReadOnlyCollection<Menu>> GetAll()
        {
            var menusList = await _context.MenusSet.ToListAsync();
            return (from m in menusList select _menuMapper.DbToDomain(m)).ToList().AsReadOnly();
        }

        public async Task<Menu> GetOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var menuDbModel = await _context.MenusSet.FindAsync(id);
                return _menuMapper.DbToDomain(menuDbModel);
            }
            else
            {
                return null;
            }
        }

        public async Task<Menu> AddOne(Menu item)
        {
            var exists = await Exists(item.Id);
            if (!exists)
            {
                await _context.MenusSet.AddAsync(_menuMapper.DomainToDb(item));
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task<Menu> UpdateOne(Menu item)
        {
            var exists = await Exists(item.Id);
            if (exists)
            {
                await Detach(item.Id);
                var enState = _context.MenusSet.Update(_menuMapper.DomainToDb(item));
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
                var menuDbModel = await _context.MenusSet.FindAsync(id);
                _context.MenusSet.Remove(menuDbModel);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.MenusSet.AnyAsync(menu => menu.Id.Equals(id));
        }

        public async Task Detach(Guid id)
        {
            var entity = await _context.MenusSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
