using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LMS.Models;

namespace LMS.Controllers
{
    public class BorrowRecordController : Controller
    {
        private readonly NeondbContext _context;

        public BorrowRecordController(NeondbContext context)
        {
            _context = context;
        }

        // GET: BorrowRecord
        public async Task<IActionResult> Index()
        {
            var neondbContext = _context.BorrowRecords.Include(b => b.Book).Include(b => b.Member);
            return View(await neondbContext.ToListAsync());
        }

        // GET: BorrowRecord/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowRecord = await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.Recordid == id);
            if (borrowRecord == null)
            {
                return NotFound();
            }

            return View(borrowRecord);
        }

        // GET: BorrowRecord/Create
        public IActionResult Create()
        {
            ViewData["Bookid"] = new SelectList(_context.Books, "Bookid", "Bookid");
            ViewData["Memberid"] = new SelectList(_context.Members, "Memberid", "Memberid");
            return View();
        }

        // POST: BorrowRecord/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Recordid,Memberid,Bookid,Borrowdate,Returndate")] BorrowRecord borrowRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Bookid"] = new SelectList(_context.Books, "Bookid", "Bookid", borrowRecord.Bookid);
            ViewData["Memberid"] = new SelectList(_context.Members, "Memberid", "Memberid", borrowRecord.Memberid);
            return View(borrowRecord);
        }

        // GET: BorrowRecord/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowRecord = await _context.BorrowRecords.FindAsync(id);
            if (borrowRecord == null)
            {
                return NotFound();
            }
            ViewData["Bookid"] = new SelectList(_context.Books, "Bookid", "Bookid", borrowRecord.Bookid);
            ViewData["Memberid"] = new SelectList(_context.Members, "Memberid", "Memberid", borrowRecord.Memberid);
            return View(borrowRecord);
        }

        // POST: BorrowRecord/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Recordid,Memberid,Bookid,Borrowdate,Returndate")] BorrowRecord borrowRecord)
        {
            if (id != borrowRecord.Recordid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowRecordExists(borrowRecord.Recordid))
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
            ViewData["Bookid"] = new SelectList(_context.Books, "Bookid", "Bookid", borrowRecord.Bookid);
            ViewData["Memberid"] = new SelectList(_context.Members, "Memberid", "Memberid", borrowRecord.Memberid);
            return View(borrowRecord);
        }

        // GET: BorrowRecord/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowRecord = await _context.BorrowRecords
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.Recordid == id);
            if (borrowRecord == null)
            {
                return NotFound();
            }

            return View(borrowRecord);
        }

        // POST: BorrowRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowRecord = await _context.BorrowRecords.FindAsync(id);
            if (borrowRecord != null)
            {
                _context.BorrowRecords.Remove(borrowRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowRecordExists(int id)
        {
            return _context.BorrowRecords.Any(e => e.Recordid == id);
        }
    }
}
