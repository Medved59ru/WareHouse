using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WareHouse.BAL;
using WareHouse.DataAccessLayer;
using WareHouse.DataAccessLayer.Models;

namespace WareHouse.Controllers
{
    public class StoredItemsController : Controller
    {
        private readonly EFStoredItemHandler _storedItem;

        public StoredItemsController(EFStoredItemHandler storedItem)
        {
            _storedItem = storedItem;
        }

        // GET: StoredItems
        public async Task<IActionResult> Index()
        {
            var applicationContext = _storedItem.GetStoredItems();
            return View(await applicationContext.ToListAsync());
        }

        public async Task<IActionResult> StoreList(int? id)
        {
            var applicationContext = _storedItem.GetStoredItemsByStores(id);
            return View(await applicationContext.ToListAsync());
        }

        // GET: StoredItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storedItem = await _storedItem.GetStoredItemBy(id);
            if (storedItem == null)
            {
                return NotFound();
            }

            return View(storedItem);
        }

        // GET: StoredItems/Create
        public IActionResult Create()
        {
            ViewData["ItemId"] = _storedItem.GetItemsForList();
            ViewData["StoreId"] = _storedItem.GetStoresForList();
            return View();
        }

        // POST: StoredItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemId,StoreId,PlacePosition,Amount")] StoredItem storedItem)
        {
            if (ModelState.IsValid)
            {
                var success = await _storedItem.Save(storedItem);
               if(success)
                  return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = _storedItem.GetItemsForList("Id", "Name", storedItem.ItemId); 
            ViewData["StoreId"] =  _storedItem.GetStoresForList("Id", "Name", storedItem.StoreId);
            return View(storedItem);
        }

        // GET: StoredItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storedItem = await _storedItem.GetStoredItemBy(id);
              
            if (storedItem == null)
            {
                return NotFound();
            }
            ViewData["ItemId"] =_storedItem.GetItemsForList("Id", "Name", storedItem.ItemId);
            ViewData["StoreId"] = _storedItem.GetStoresForList( "Id", "Name", storedItem.StoreId); 
            return View(storedItem);
        }

        // POST: StoredItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemId,StoreId,PlacePosition,Amount")] StoredItem storedItem)
        {
            if (id != storedItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var success = await _storedItem.Update(storedItem);

                if(success)
                   return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = _storedItem.GetItemsForList("Id", "Name", storedItem.ItemId);
            ViewData["StoreId"] = _storedItem.GetStoresForList("Id", "Name", storedItem.StoreId);
            return View(storedItem);
        }

        // GET: StoredItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storedItem = await _storedItem.GetStoredItemBy(id);

            if (storedItem == null)
            {
                return NotFound();
            }

            return View(storedItem);
        }

        // POST: StoredItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storedItem = await _storedItem.FindStoredItemBy(id);
            var success = await _storedItem.Delete(storedItem);
            if (success)
                return RedirectToAction(nameof(Index));

            return Content("ОШИБКА ОПЕРАЦИИ. Есть всязанные данные => удалить нельзя");
            
        }

       
    }
}
