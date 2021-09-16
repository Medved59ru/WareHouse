using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.DataAccessLayer;
using WareHouse.DataAccessLayer.Models;

namespace WareHouse.BAL
{
    public class EFStoreHandler
    {
        private readonly ApplicationContext _context;
        public EFStoreHandler(ApplicationContext context)
        {
            _context = context;
        }
        public IQueryable<Store> GetStores () => _context.Stores;

        public async Task<Store> GetStoreBy(int? id) => await _context.Stores.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<bool> SaveStore(Store store)
        {
            bool success;
            try
            {
                _context.Stores.Add(store);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }

        public async Task<Store> FindStoreBy(int? id) => await _context.Stores.FindAsync(id);

        public async Task<bool> Update(Store store)
        {
            bool success;
            try
            {
                _context.Update(store);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                success = true;
            }
            if (!StoreExists(store.Id))
            {
                success = false;
            }
            return success;
        }

        public async Task<bool> Delete(Store store)
        {
            bool success;
            try
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }



        private bool StoreExists(int id)
        {
            return _context.Stores.Any(e => e.Id == id);
        }
    }
}
