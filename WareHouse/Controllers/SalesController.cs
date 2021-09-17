using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WareHouse.DataAccessLayer;
using WareHouse.DataAccessLayer.Models;
using WareHouse.BAL;

namespace WareHouse.Controllers
{
    public class SalesController : Controller
    {
        private readonly EFSalesHandler _sale;

        public SalesController(EFSalesHandler sale)
        {
            _sale = sale;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            var salesList = _sale.GetSales();
            return View(await salesList.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _sale.GetSaleBy(id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["BuyerId"] = _sale.GetBuyersForList();
            ViewData["ItemId"] = _sale.GetItemsForList();
            ViewData["StoreId"] = _sale.GetStoresForList();
            ViewData["TypeOfDocumentId"] = _sale.GetTypeOfDocForList();
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
                var success = await _sale.SaveSale(sale);
                if(success)
                      return RedirectToAction(nameof(Index));
            }
            ViewData["BuyerId"] =  _sale.GetBuyersForList("Id", "Name", sale.BuyerId);
            ViewData["ItemId"] =  _sale.GetItemsForList("Id", "Name", sale.ItemId);
            ViewData["StoreId"] = _sale.GetStoresForList("Id", "Name", sale.StoreId);
            ViewData["TypeOfDocumentId"] = _sale.GetTypeOfDocForList("Id", "Name", sale.TypeOfDocumentId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _sale.FindSaleBy(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["BuyerId"] = _sale.GetBuyersForList("Id", "Name", sale.BuyerId);
            ViewData["ItemId"] = _sale.GetItemsForList("Id", "Name", sale.ItemId);
            ViewData["StoreId"] = _sale.GetStoresForList("Id", "Name", sale.StoreId);
            ViewData["TypeOfDocumentId"] = _sale.GetTypeOfDocForList("Id", "Name", sale.TypeOfDocumentId);
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
                var success =await _sale.Update(sale);
                if(success)
                        return RedirectToAction(nameof(Index));
            }
            ViewData["BuyerId"] = _sale.GetBuyersForList("Id", "Name", sale.BuyerId);
            ViewData["ItemId"] = _sale.GetItemsForList("Id", "Name", sale.ItemId);
            ViewData["StoreId"] = _sale.GetStoresForList("Id", "Name", sale.StoreId);
            ViewData["TypeOfDocumentId"] = _sale.GetTypeOfDocForList("Id", "Name", sale.TypeOfDocumentId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _sale.GetSaleBy(id);
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
            var sale = await _sale.FindSaleBy(id);
            var success = await _sale.Delete(sale);
            if(success)
               return RedirectToAction(nameof(Index));

            return Content("ОШИБКА ОПЕРАЦИИ. ПРОВЕРЬТЕ ПРАВИЛЬНОСТЬ ДЕЙСТВИЙ");
        }

       
    }
}
