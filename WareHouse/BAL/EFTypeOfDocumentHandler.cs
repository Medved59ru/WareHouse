using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.DataAccessLayer;
using WareHouse.DataAccessLayer.Models;

namespace WareHouse.BAL
{
    public class EFTypeOfDocumentHandler
    {
        private readonly ApplicationContext _context;

        public EFTypeOfDocumentHandler(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<TypeOfDocument> GetTypeOfDocuments() => _context.TypeOfDocuments;

        public async Task<TypeOfDocument> GetTypeOfDocumentBy(int? id) => await _context.TypeOfDocuments.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<TypeOfDocument> FindTypeOfDocumentsBy(int? id) => await _context.TypeOfDocuments.FindAsync(id);

        public async Task<bool> SaveTypeOfDocuments(TypeOfDocument typeOfDocument)
        {
            bool success;
            try
            {
                _context.TypeOfDocuments.Add(typeOfDocument);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch
            {
                success = false;
            }

            return success;

        }

        public async Task<bool> Update(TypeOfDocument typeOfDocument)
        {
            bool success;
            try
            {
                _context.Update(typeOfDocument);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                success = true;
            }
            if (!ItemExists(typeOfDocument.Id))
            {
                success = false;
            }
            return success;
        }

        public async Task<bool> Delete(TypeOfDocument item)
        {
            bool success;
            try
            {
                _context.TypeOfDocuments.Remove(item);
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
            return _context.TypeOfDocuments.Any(e => e.Id == id);
        }

    }
}
