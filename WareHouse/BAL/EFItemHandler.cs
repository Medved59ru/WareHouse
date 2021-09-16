using System.Linq;
using System.Threading.Tasks;
using WareHouse.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using WareHouse.DataAccessLayer.Models;

namespace WareHouse.BAL
{
    public class EFItemHandler
    {
        private readonly ApplicationContext _context;

        public EFItemHandler(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Item> GetItems() => _context.Items;

        public async Task<Item> GetItemBy(int? id) => await _context.Items.FirstOrDefaultAsync(m => m.Id == id);
                
        public async Task<Item> FindItemBy(int? id) => await _context.Items.FindAsync(id);

        public async Task<bool> SaveItem(Item item)
        {
            bool success;
            try
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }

            return success;

        }

        public async Task<bool> Update(Item item)
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
            if (!ItemExists(item.Id))
            {
                success = false;
            }
            return success;
        }

        public async Task<bool> Delete(Item item)
        {
            bool success;
            try
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
