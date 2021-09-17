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
    public class EFSuppliesHandler
    {
        private readonly ApplicationContext _context;
        public EFSuppliesHandler(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Supply> GetSupplies() => _context.Supplies
                                                    .Include(s => s.Item)
                                                    .Include(s => s.Store)
                                                    .Include(s => s.TypeOfDocument)
                                                    .Include(s => s.Vendor);

        public async Task<Supply> GetSupplyBy(int? id) => await _context.Supplies
                                                                .Include(s => s.Item)
                                                                .Include(s => s.Store)
                                                                .Include(s => s.TypeOfDocument)
                                                                .Include(s => s.Vendor)
                                                                .FirstOrDefaultAsync(m => m.Id == id);

        public SelectList GetItemsForList(string id = "Id", string name = "Name", int? foriengId = null)
            => new SelectList(_context.Items, id, name, foriengId);

        public SelectList GetStoresForList(string id = "Id", string name = "Name", int? foriengId = null)
            => new SelectList(_context.Stores, id, name, foriengId);

        public SelectList GetTypeOfDocumentsForList(string id = "Id", string name = "Name", int? foriengId = null)
            => new SelectList(_context.TypeOfDocuments, id, name, foriengId);

        public SelectList GetVendorsForList(string id = "Id", string name = "Name", int? foriengId = null)
            => new SelectList(_context.Vendors, id, name, foriengId);


        public async Task<bool> SaveSupply(Supply supply)
        {
            bool success;
            try
            {
                UpdateStoredItemsBy(supply.ItemId, supply.StoreId, supply.Amount);
                _context.Add(supply);
                await _context.SaveChangesAsync();
                success = true;
               
            }
            catch
            {
                success = false;
            }
            return success;

        }

        public async Task<Supply> FindSupplyBy(int? id) => await _context.Supplies.FindAsync(id);

        public async Task<bool> Update(Supply supply)
        {
            bool success;
            try
            {
                _context.Update(supply);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                success = true;
            }
            if (!SupplyExists(supply.Id))
            {
                success = false;
            }
            return success;
        }

        public async Task<bool> Delete(Supply supply)
        {

            bool success;
            try
            {
                _context.Supplies.Remove(supply);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }

        private bool SupplyExists(int id)
        {
            return _context.Supplies.Any(e => e.Id == id);
        }

        private void UpdateStoredItemsBy(int? itemId, int? storeId, int amount)
        {
            var item = _context.StoredItems.Include(g => g.Store).Where(p => p.StoreId == storeId)
                                           .Include(g => g.Item).Where(p => p.ItemId == itemId).FirstOrDefault();
           
                                  
            if (item == null)
            {
                StoredItem storedItem = new StoredItem() { ItemId = itemId, StoreId = storeId, Amount = amount };
                _context.Add(storedItem);
               
            }
            else
            {
                item.Amount += amount;
            }
            _context.SaveChanges();

        }
       
    }
}
