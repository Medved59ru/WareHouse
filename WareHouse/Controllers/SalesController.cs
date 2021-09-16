using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WareHouse.DataAccessLayer;
using WareHouse.DataAccessLayer.Models;

namespace WareHouse.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationContext _context;

        public SalesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Sales.Include(s => s.Buyer).Include(s => s.Item).Include(s => s.Store).Include(s => s.TypeOfDocument);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Buyer)
                .Include(s => s.Item)
                .Include(s => s.Store)
                .Include(s => s.TypeOfDocument)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["BuyerId"] = new SelectList(_context.Buyers, "Id", "Name");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name");
            ViewData["TypeOfDocumentId"] = new SelectList(_context.TypeOfDocuments, "Id", "Name");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Date,TypeOfDocumentId,BuyerId,ItemId,StoreId,Amount")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuyerId"] = new SelectList(_context.Buyers, "Id", "Name", sale.BuyerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", sale.ItemId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name", sale.StoreId);
            ViewData["TypeOfDocumentId"] = new SelectList(_context.TypeOfDocuments, "Id", "Name", sale.TypeOfDocumentId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["BuyerId"] = new SelectList(_context.Buyers, "Id", "Name", sale.BuyerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", sale.ItemId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name", sale.StoreId);
            ViewData["TypeOfDocumentId"] = new SelectList(_context.TypeOfDocuments, "Id", "Name", sale.TypeOfDocumentId);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Date,TypeOfDocumentId,BuyerId,ItemId,StoreId,Amount")] Sale sale)
        {
            if (id != sale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleExists(sale.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuyerId"] = new SelectList(_context.Buyers, "Id", "Name", sale.BuyerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", sale.ItemId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "Id", "Name", sale.StoreId);
            ViewData["TypeOfDocumentId"] = new SelectList(_context.TypeOfDocuments, "Id", "Name", sale.TypeOfDocumentId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Buyer)
                .Include(s => s.Item)
                .Include(s => s.Store)
                .Include(s => s.TypeOfDocument)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
