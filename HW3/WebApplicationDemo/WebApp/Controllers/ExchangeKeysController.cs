using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApp.Data;

namespace WebApp.Controllers
{
    public class ExchangeKeysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExchangeKeysController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        string GetUserId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return claim.Value ?? "";
        }

        // GET: ExchangeKeys
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            return View(await _context.ExchangeKeys.Where(c =>c.UserId == userId).ToListAsync());
        }

        // GET: ExchangeKeys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            if (!IdExists(id))
                return RedirectToAction("Index");

            var exchangeKey = await _context.ExchangeKeys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exchangeKey == null)
            {
                return NotFound();
            }

            return View(exchangeKey);
        }

        // GET: ExchangeKeys/Create
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: ExchangeKeys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExchangeKey exchangeKey)
        {
            if (!Crypto.KeyExchange.PrimesValidation(exchangeKey.G))
                ModelState.AddModelError("G","G must be a prime number");
            if (!Crypto.KeyExchange.PrimesValidation(exchangeKey.P))
                ModelState.AddModelError("P","P must be a prime number");

            if (ModelState.IsValid)
            {
                Crypto.KeyExchange.KeyExchangeImplementation(exchangeKey);
                exchangeKey.UserId = GetUserId();
                _context.Add(exchangeKey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exchangeKey);
        }

        // GET: ExchangeKeys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            if (!IdExists(id))
                return RedirectToAction("Index");

            var exchangeKey = await _context.ExchangeKeys.FindAsync(id);
            if (exchangeKey == null)
            {
                return NotFound();
            }
            return View(exchangeKey);
        }

        // POST: ExchangeKeys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExchangeKey exchangeKey)
        {
            if (id != exchangeKey.Id)
            {
                return NotFound();
            }
            if (!IdExists(id))
                return RedirectToAction("Index");

            if (!Crypto.KeyExchange.PrimesValidation(exchangeKey.G))
                ModelState.AddModelError("G","G must be a prime number");
            if (!Crypto.KeyExchange.PrimesValidation(exchangeKey.P))
                ModelState.AddModelError("P","P must be a prime number");
            
            if (ModelState.IsValid)
            {
                try
                {
                    exchangeKey.UserId = GetUserId();
                    _context.Update(exchangeKey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExchangeKeyExists(exchangeKey.Id))
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
            return View(exchangeKey);
        }

        // GET: ExchangeKeys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!IdExists(id))
                return RedirectToAction("Index");

            var exchangeKey = await _context.ExchangeKeys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exchangeKey == null)
            {
                return NotFound();
            }

            return View(exchangeKey);
        }

        // POST: ExchangeKeys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IdExists(id))
                return RedirectToAction("Index");
            var exchangeKey = await _context.ExchangeKeys.FindAsync(id);
            _context.ExchangeKeys.Remove(exchangeKey);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExchangeKeyExists(int id)
        {
            return _context.ExchangeKeys.Any(e => e.Id == id);
        }
        private bool IdExists(int ?id)
        {
            return _context.RsaKeys.Any(c => c.Id == id && c.UserId == GetUserId());
        }
    }
}
