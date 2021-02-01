﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly CatalogContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public ItemRepository(CatalogContext context)
        {
            _context = context ?? throw new  ArgumentNullException(nameof(context));
        }
       
        public async Task<IEnumerable<Item>> GetAsync()
        {
            return await _context
                .Items
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Item> GetAsync(Guid id)
        {
            var item = await _context.Items
                 .AsNoTracking()
                 .Where(x => x.Id == id)
                 .Include(x => x.Genre)
                 .Include(x => x.Artist).FirstOrDefaultAsync();

            return item;
        }

        public Item Add(Item order)
        {
            return _context.Items.Add(order).Entity;
        }

        public Item Update(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return item;
        }
    }
}
