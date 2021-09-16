using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.DataAccessLayer;
using WareHouse.DataAccessLayer.Models;

namespace WareHouse.BAL
{
    public class EFStoredItemHandler
    {
        private readonly ApplicationContext _context;
        public EFStoredItemHandler(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<StoredItem> GetStoredItems() => _context.StoredItems
                                                                  .Include(s => s.Item)
                                                                  .Include(s => s.Store);

        public IQueryable<StoredItem> GetStoredItemsByStores(int? id) => _context.StoredItems
                                                                                 .Include(s => s.Item)
                                                                                 .Include(g => g.Store)
                                                                                  .Where(g => g.StoreId == id);

        public async Task<StoredItem> GetStoredItemBy(int? id) => await _context.StoredItems
                                                                                .Include(s => s.Item)
                                                                                .Include(s => s.Store)
                                                                                .FirstOrDefaultAsync(m => m.Id == id);

        public SelectList GetItemsForList(string id = "Id", string name = "Name", int? foriengId = null)
            => new SelectList(_context.Items, id, name, foriengId);

        public SelectList GetStoresForList(string id = "Id", string name = "Name", int? foriengId = null)
            => new SelectList(_context.Stores, id, name, foriengId);

        public async Task<bool> Save(StoredItem item)
        {

            bool success;
            try
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;

        }

        public async Task<bool> Update(StoredItem item)
        {
            bool success;
            try
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                success = true;
            }
            if (!StoredItemExists(item.Id))
            {
                success = false;
            }
            return success;
        }

        public async Task<StoredItem> FindStoredItemBy(int? id) => await _context.StoredItems.FindAsync(id);

        public async Task<bool> Delete(StoredItem item)
        {

            bool success;
            try
            {
                _context.StoredItems.Remove(item);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }

        private bool StoredItemExists(int id)
        {
            return _context.StoredItems.Any(e => e.Id == id);
        }
    }
}
