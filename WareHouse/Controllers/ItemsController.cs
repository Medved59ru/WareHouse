using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WareHouse.DataAccessLayer.Models;
using WareHouse.BAL;

namespace WareHouse.Controllers
{
    public class ItemsController : Controller
    {
        private EFItemHandler _itemHandler; 

        public ItemsController(EFItemHandler handler)
        {
           _itemHandler = handler;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var result = _itemHandler.GetItems();
            return View(await result.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _itemHandler.GetItemBy(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Code")] Item item)
        {
            if (ModelState.IsValid)
            {
                var success = await _itemHandler.SaveItem(item);
                if (success)
                    return RedirectToAction(nameof(Index));

            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _itemHandler.FindItemBy(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }
            var success = await _itemHandler.Update(item);
            if (ModelState.IsValid)
            {
                if(success)
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _itemHandler.GetItemBy(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _itemHandler.FindItemBy(id);
            var success =await _itemHandler.Delete(item);

            if(success)
                    return RedirectToAction(nameof(Index));

            return Content("ОШИБКА ОПЕРАЦИИ");

        }

        
    }
}
