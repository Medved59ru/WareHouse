using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.DataAccessLayer;
using WareHouse.DataAccessLayer.Models;

namespace WareHouse.BAL
{
    public class EFBuyerHandler
    {
        private readonly ApplicationContext _context;
        public EFBuyerHandler (ApplicationContext context)
        {
            _context = context;
        }
        public IQueryable<Buyer> GetBuyers() => _context.Buyers;

        public async Task<Buyer> GetBuyerBy(int? id) => await _context.Buyers.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<bool> SaveBuyer(Buyer buyer)
        {
            bool success;
            try
            {
                _context.Buyers.Add(buyer);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }

        public async Task<Buyer> FindBuyerBy(int? id) => await _context.Buyers.FindAsync(id);

        public async Task<bool> Update(Buyer buyer)
        {
            bool success;
            try
            {
                _context.Update(buyer);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                success = true;
            }
            if (!BuyerExists(buyer.Id))
            {
                success = false;
            }
            return success;
        }

        public async Task<bool> Delete(Buyer buyer)
        {
            bool success;
            try
            {
                _context.Buyers.Remove(buyer);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }



        private bool BuyerExists(int id)
        {
            return _context.Buyers.Any(e => e.Id == id);
        }
    }
}
