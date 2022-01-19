using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;

namespace WebApp.Controllers
{
    [Authorize]
    public class CesarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CesarsController(ApplicationDbContext context)
        {

            _context = context;
        }


        public string GetUserId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return claim?.Value ?? "";
        }

        // GET: Cesars
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            return View(await _context.Cesars.Where(c => c.UserId == userId).ToListAsync());

        }

        // GET: Cesars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cesar = await _context.Cesars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cesar == null)
            {
                return NotFound();
            }

            return View(cesar);
        }

        // GET: Cesars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cesars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cesar cesar)
        {
            
            ValidateCesar(cesar);

            if (ModelState.IsValid)
            {
                cesar.Key = cesar.Key % 255;
                cesar.CypherText = System.Convert.ToBase64String(Crypto.Cesar.Encrypt(cesar.PlainText, (byte) cesar.Key, Encoding.Default));
                cesar.UserId = GetUserId();
                _context.Add(cesar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cesar);
        }

        // GET: Cesars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cesar = await _context.Cesars.FindAsync(id);
            if (cesar == null)
            {
                return NotFound();
            }
            return View(cesar);
        }

        public void ValidateCesar(Cesar cesar)
        {
            if (cesar.Key == 0)
            {
                ModelState.AddModelError(nameof(Cesar.Key), "Cesar key cannot be 0");
            }
            if (string.IsNullOrWhiteSpace(cesar.PlainText))
            {
                ModelState.AddModelError(nameof(Cesar.PlainText), "Please write something!");
            }
        }

        // POST: Cesars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cesar cesar)
        {
            if (id != cesar.Id)
            {
                return NotFound();
            }

            ValidateCesar(cesar);

            cesar.UserId = GetUserId();
            
            if (ModelState.IsValid)
            {
                cesar.Key = cesar.Key % 255;
                cesar.CypherText = System.Convert.ToBase64String(Crypto.Cesar.Encrypt(cesar.PlainText, (byte) cesar.Key, Encoding.Default));

                try
                {
                    _context.Update(cesar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CesarExists(cesar.Id))
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
            return View(cesar);
        }

        // GET: Cesars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cesar = await _context.Cesars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cesar == null)
            {
                return NotFound();
            }

            return View(cesar);
        }

        // POST: Cesars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // TODO: .where(c => c.Id == id && c.UserId == GetUserId()
            var cesar = await _context.Cesars.FindAsync(id);
            
            _context.Cesars.Remove(cesar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CesarExists(int id)
        {
            return _context.Cesars.Any(e => e.Id == id);
        }
    }
}
