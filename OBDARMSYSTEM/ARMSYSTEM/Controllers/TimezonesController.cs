using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ARMSYSTEM.Models;

namespace ARMSYSTEM.Controllers
{
    public class TimezonesController : Controller
    {
        private readonly PhonesContext db;

        public TimezonesController(PhonesContext context)
        {
            db = context;
        }

        // GET: Timezones
        public async Task<IActionResult> Index()
        {
            return View(await db.Timezone.ToListAsync());
        }

        // GET: Timezones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timezone = await db.Timezone
                .SingleOrDefaultAsync(m => m.id == id);
            if (timezone == null)
            {
                return NotFound();
            }

            return View(timezone);
        }

        // GET: Timezones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Timezones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,city,region,FO,population,timezone")] Timezone timezone)
        {
            if (ModelState.IsValid)
            {
                db.Add(timezone);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timezone);
        }

        // GET: Timezones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timezone = await db.Timezone.SingleOrDefaultAsync(m => m.id == id);
            if (timezone == null)
            {
                return NotFound();
            }
            return View(timezone);
        }

        // POST: Timezones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,city,region,FO,population,timezone")] Timezone timezone)
        {
            if (id != timezone.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(timezone);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimezoneExists(timezone.id))
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
            return View(timezone);
        }

        // GET: Timezones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timezone = await db.Timezone
                .SingleOrDefaultAsync(m => m.id == id);
            if (timezone == null)
            {
                return NotFound();
            }

            return View(timezone);
        }

        // POST: Timezones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timezone = await db.Timezone.SingleOrDefaultAsync(m => m.id == id);
            db.Timezone.Remove(timezone);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimezoneExists(int id)
        {
            return db.Timezone.Any(e => e.id == id);
        }
    }
}
