using FastFood.Domain.Entities;
using FastFood.Domain.Interfaces;
using FastFood.Repo.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Infrastructure.Implementation
{
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(DataContext context) : base(context)
        {
        }

        public async Task<ICollection<MenuItem>> GetMenuItemsByRestaurant(int restaurantId)
        {
            return await _context.MenuItems.Where(m => m.RestaurantId == restaurantId).ToListAsync();
        }
    }
}
