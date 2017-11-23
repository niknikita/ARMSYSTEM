using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ARMSYSTEM.Models;
using ARMSYSTEM.Services.FiltersFizPhone;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;

namespace ARMSYSTEM.Controllers
{
    public class FiltersController : Controller
    {
        private readonly PhonesContext db;

        public FiltersController(PhonesContext context)
        {
            db = context;
        }

        // GET: Filters
        public async Task<IActionResult> Index()
        {
            return View(await db.Filter.ToListAsync());
        }
        public ActionResult FilterRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(db.Filter.ToDataSourceResult(request));
        }
        public ActionResult FilterTemplateRead([DataSourceRequest] DataSourceRequest request)
        {
            var result = new List<FilterTemplate>() {
                new FilterTemplate()
                {
                    Cities = new List<string>() { "Екатеринбург", "Челябинск" },
                    DataTimeRangeFinish = DateTime.Now,
                    DataTimeRangeStart = DateTime.Now,
                    DateTimeRange = true,
                    Include = false,
                    Id = 1,
                    IsMobile = true,
                    IsStatic = true,
                    Streets = new List<string>(){ "Победы","Кирова", "Свердловский тракт","пр-кт победы" }
                },
                new FilterTemplate()
                {
                    Cities = new List<string>() { "Екатеринбург", "Челябинск" },
                    DataTimeRangeFinish = DateTime.Now,
                    DataTimeRangeStart = DateTime.Now,
                    DateTimeRange = false,
                    Include = true,
                    Id = 2,
                    IsMobile = false,
                    IsStatic = true,
                    Streets = null
                }

            };
            return Json(result.ToDataSourceResult(request));
        }

        [AcceptVerbs("Post")]
        public ActionResult SaveFiltersTemplate([DataSourceRequest] DataSourceRequest request,  [Bind(Prefix = "models")]IEnumerable<FilterTemplate> template)
        {
            var result = new List<FilterTemplate>() {
                new FilterTemplate()
                {
                    Cities = new List<string>() { "Екатеринбург", "Челябинск" },
                    DataTimeRangeFinish = DateTime.Now,
                    DataTimeRangeStart = DateTime.Now,
                    DateTimeRange = true,
                    Include = false,
                    Id = 1,
                    IsMobile = true,
                    IsStatic = true,
                    Streets = new List<string>(){ "Победы","Кирова", "Свердловский тракт","пр-кт победы" }
                },
                new FilterTemplate()
                {
                    Cities = new List<string>() { "Екатеринбург", "Челябинск" },
                    DataTimeRangeFinish = DateTime.Now,
                    DataTimeRangeStart = DateTime.Now,
                    DateTimeRange = false,
                    Include = true,
                    Id = 2,
                    IsMobile = false,
                    IsStatic = true,
                    Streets = null
                }

            };
            return Json(result.ToDataSourceResult(request, ModelState));
        }

        // GET: Filters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filter = await db.Filter
                .SingleOrDefaultAsync(m => m.id == id);
            if (filter == null)
            {
                return NotFound();
            }

            return View(filter);
        }

        // GET: Filters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Filters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name")] Filter filter, [Bind("Include,IsStatic,IsMobile,DateTimeRange,DataTimeRangeStart,DataTimeRangeFinish,Cities,Streets")]FilterTemplate filterTemplate)
        {
            if (ModelState.IsValid)
            {
                filter.Template = JsonConvert.SerializeObject(filterTemplate);
                db.Filter.Add(filter);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(filter);
        }

        // GET: Filters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filter = await db.Filter.SingleOrDefaultAsync(m => m.id == id);
            if (filter == null)
            {
                return NotFound();
            }
            return View(filter);
        }

        // POST: Filters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Template")] Filter filter)
        {
            if (id != filter.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(filter);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilterExists(filter.id))
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
            return View(filter);
        }

        // GET: Filters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filter = await db.Filter
                .SingleOrDefaultAsync(m => m.id == id);
            if (filter == null)
            {
                return NotFound();
            }

            return View(filter);
        }

        // POST: Filters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filter = await db.Filter.SingleOrDefaultAsync(m => m.id == id);
            db.Filter.Remove(filter);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilterExists(int id)
        {
            return db.Filter.Any(e => e.id == id);
        }
    }
}
