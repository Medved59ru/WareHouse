using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WareHouse.BAL;
using WareHouse.DataAccessLayer.Models;

namespace WareHouse.Controllers
{
    public class TypeOfDocumentsController : Controller
    {
        private readonly EFTypeOfDocumentHandler _type;

        public TypeOfDocumentsController(EFTypeOfDocumentHandler type)
        {
            _type = type;
        }

        // GET: TypeOfDocuments
        public async Task<IActionResult> Index()
        {

            return View(await _type.GetTypeOfDocuments().ToListAsync());
        }

        // GET: TypeOfDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfDocument = await _type.GetTypeOfDocumentBy(id);
            if (typeOfDocument == null)
            {
                return NotFound();
            }

            return View(typeOfDocument);
        }

        // GET: TypeOfDocuments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Operation,Name")] TypeOfDocument typeOfDocument)
        {
            if (ModelState.IsValid)
            {
                var success =await _type.SaveTypeOfDocuments(typeOfDocument);
                if(success)
                    return RedirectToAction(nameof(Index));
            }
            return View(typeOfDocument);
        }

        // GET: TypeOfDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfDocument = await _type.FindTypeOfDocumentsBy(id);
            if (typeOfDocument == null)
            {
                return NotFound();
            }
            return View(typeOfDocument);
        }

        // POST: TypeOfDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Operation,Name")] TypeOfDocument typeOfDocument)
        {
            if (id != typeOfDocument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var success = _type.Update(typeOfDocument);
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfDocument);
        }

        // GET: TypeOfDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfDocument = await _type.GetTypeOfDocumentBy(id);
            if (typeOfDocument == null)
            {
                return NotFound();
            }

            return View(typeOfDocument);
        }

        // POST: TypeOfDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeOfDocument = await _type.GetTypeOfDocumentBy(id);
            var success =await _type.Delete(typeOfDocument);
            if(success)
                return RedirectToAction(nameof(Index));

            return Content("ОШИБКА ОПЕРАЦИИ");
        }

       
    }
}
