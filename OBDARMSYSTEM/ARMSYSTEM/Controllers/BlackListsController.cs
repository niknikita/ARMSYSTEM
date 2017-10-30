using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ARMSYSTEM.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace ARMSYSTEM.Controllers
{
    public class BlackListsController : Controller
    {
        private readonly PhonesContext db;

        public BlackListsController(PhonesContext context)
        {
            db = context;
        }

        // GET: BlackLists
        public async Task<IActionResult> Index()
        {
            return View(await db.BlackList.ToListAsync());
        }
        public ActionResult BlackListRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(db.BlackList.ToDataSourceResult(request));
        }


        // GET: BlackLists/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blackList = await db.BlackList
                .SingleOrDefaultAsync(m => m.id == id);
            if (blackList == null)
            {
                return NotFound();
            }

            return View(blackList);
        }

        // GET: BlackLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlackLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,phone,descripton")] BlackList blackList)
        {
            if (ModelState.IsValid)
            {
                db.Add(blackList);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blackList);
        }

        // GET: BlackLists/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blackList = await db.BlackList.SingleOrDefaultAsync(m => m.id == id);
            if (blackList == null)
            {
                return NotFound();
            }
            return View(blackList);
        }

        // POST: BlackLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("id,phone,descripton")] BlackList blackList)
        {
            if (id != blackList.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(blackList);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlackListExists(blackList.id))
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
            return View(blackList);
        }

        // GET: BlackLists/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blackList = await db.BlackList
                .SingleOrDefaultAsync(m => m.id == id);
            if (blackList == null)
            {
                return NotFound();
            }

            return View(blackList);
        }

        // POST: BlackLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var blackList = await db.BlackList.SingleOrDefaultAsync(m => m.id == id);
            db.BlackList.Remove(blackList);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlackListExists(long id)
        {
            return db.BlackList.Any(e => e.id == id);
        }
    }
}
