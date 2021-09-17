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
    public class SuppliesController : Controller
    {
        private readonly EFSuppliesHandler _supply;

        public SuppliesController(EFSuppliesHandler supply)
        {
            _supply = supply;
        }

        // GET: Supplies
        public async Task<IActionResult> Index()
        {
            var applicationContext = _supply.GetSupplies();
            return View(await applicationContext.ToListAsync());
        }

        // GET: Supplies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supply = await _supply.GetSupplyBy(id);
            if (supply == null)
            {
                return NotFound();
            }

            return View(supply);
        }

        // GET: Supplies/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = _supply.GetItemsForList();
            ViewData["StoreId"] = _supply.GetStoresForList();
            ViewData["TypeOfDocumentId"] =_supply.GetTypeOfDocumentsForList();
            ViewData["VendorId"] = _supply.GetVendorsForList();
            return View();
        }

        // POST: Supplies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Date,TypeOfDocumentId,VendorId,ItemId,StoreId,Amount")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                var sucess =await _supply.SaveSupply(supply);
                if(sucess)
                   return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = _supply.GetItemsForList("Id", "Name", supply.ItemId);
            ViewData["StoreId"] =  _supply.GetStoresForList("Id", "Name", supply.StoreId);
            ViewData["TypeOfDocumentId"] = _supply.GetTypeOfDocumentsForList("Id", "Name", supply.TypeOfDocumentId);
            ViewData["VendorId"] = _supply.GetVendorsForList("Id", "Name", supply.VendorId);
            return View(supply);
        }

        // GET: Supplies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supply = await _supply.FindSupplyBy(id);
            if (supply == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] = _supply.GetItemsForList("Id", "Name", supply.ItemId);
            ViewData["StoreId"] = _supply.GetStoresForList("Id", "Name", supply.StoreId);
            ViewData["TypeOfDocumentId"] = _supply.GetTypeOfDocumentsForList("Id", "Name", supply.TypeOfDocumentId);
            ViewData["VendorId"] = _supply.GetVendorsForList("Id", "Name", supply.VendorId);
            return View(supply);
        }

        // POST: Supplies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Date,TypeOfDocumentId,VendorId,ItemId,StoreId,Amount")] Supply supply)
        {
            if (id != supply.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var success = await _supply.Update(supply);
                if(success)
                   return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = _supply.GetItemsForList("Id", "Name", supply.ItemId);
            ViewData["StoreId"] = _supply.GetStoresForList("Id", "Name", supply.StoreId);
            ViewData["TypeOfDocumentId"] = _supply.GetTypeOfDocumentsForList("Id", "Name", supply.TypeOfDocumentId);
            ViewData["VendorId"] = _supply.GetVendorsForList("Id", "Name", supply.VendorId);
            return View(supply);
        }

        // GET: Supplies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supply = await _supply.GetSupplyBy(id);
            if (supply == null)
            {
                return NotFound();
            }

            return View(supply);
        }

        // POST: Supplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supply = await _supply.FindSupplyBy(id);
            var success = await _supply.Delete(supply);
            if(success)
               return RedirectToAction(nameof(Index));

            return Content("ОШИБКА ОПЕРАЦИИ. ПРОВЕРЬТЕ ПРАВИЛЬНОСТЬ ДЕЙСТВИЙ");
        }

       
    }
}
