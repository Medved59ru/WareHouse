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
    public class EFSalesHandler
    {
        private readonly ApplicationContext _context;
        public EFSalesHandler(ApplicationContext context)
        {
            _context = context;
        }

        public  IQueryable<Sale> GetSales()=>  _context.Sales.Include(s => s.Buyer)
                                                       .Include(s => s.Item)
                                                       .Include(s => s.Store)
                                                       .Include(s => s.TypeOfDocument);

        public async Task<Sale> GetSaleBy(int? id) => await _context.Sales
                                                                        .Include(s => s.Buyer)
                                                                        .Include(s => s.Item)
                                                                        .Include(s => s.Store)
                                                                        .Include(s => s.TypeOfDocument)
                                                                        .FirstOrDefaultAsync(m => m.Id == id);

        public SelectList GetBuyersForList(string id = "Id", string name = "Name", int? foriengId = null)
                                                             => new SelectList(_context.Buyers, id, name, foriengId);

        public SelectList GetItemsForList(string id = "Id", string name = "Name", int? foriengId = null)
                                                               => new SelectList(_context.Items, id, name, foriengId);

        public SelectList GetStoresForList(string id = "Id", string name = "Name", int? foriengId = null)
                                                               =>  new SelectList(_context.Stores, id, name, foriengId);

        public SelectList GetTypeOfDocForList(string id = "Id", string name = "Name", int? foriengId = null)
                                                             => new SelectList(_context.TypeOfDocuments, id, name, foriengId);

        public async Task<bool> SaveSale(Sale sale)
        {
            bool success;
            try
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                success = true;

            }
            catch
            {
                success = false;
            }
            return success;

        }

        public async Task<Sale> FindSaleBy(int? id) => await _context.Sales.FindAsync(id);

        public async Task<bool> Update(Sale sale)
        {
            bool success;
            try
            {
                _context.Update(sale);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                success = true;
            }
            if (!SaleExists(sale.Id))
            {
                success = false;
            }
            return success;
        }

        public async Task<bool> Delete(Sale sale)
        {

            bool success;
            try
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }
            return success;
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
