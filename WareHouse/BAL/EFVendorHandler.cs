using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.DataAccessLayer;
using WareHouse.DataAccessLayer.Models;

namespace WareHouse.BAL
{
    public class EFVendorHandler
    {
        private readonly ApplicationContext _context;
        public EFVendorHandler(ApplicationContext context)
        {
            _context = context;
        }
        public IQueryable<Vendor> GetVendors() => _context.Vendors;

        public async Task<Vendor> GetVendorBy(int? id) => await _context.Vendors.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<bool> SaveVendor(Vendor vendor)
        {
            bool success;
            try
            {
                _context.Vendors.Add(vendor);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }

        public async Task<Vendor> FindVendorBy(int? id) => await _context.Vendors.FindAsync(id);

        public async Task<bool> Update(Vendor vendor)
        {
            bool success;
            try
            {
                _context.Update(vendor);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                success = true;
            }
            if (!VendorExists(vendor.Id))
            {
                success = false;
            }
            return success;
        }

        public async Task<bool> Delete(Vendor vendor)
        {
            bool success;
            try
            {
                _context.Vendors.Remove(vendor);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }



        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
    }

}

