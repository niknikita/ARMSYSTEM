using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ARMSYSTEM.Models;
using ARMSYSTEM.ViewModels;
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
            var filter = db.Filter.Where(c => c.Name == "sds");
            var filterName = HttpContext.Session.Get<string>("filterName");
            IEnumerable<FilterItemViewModel> result = null;
            if (filterName != null)
            {
                var filtertemplates = HttpContext.Session.Get<List<FilterTemplate>>(filterName);
                //var filtertemplates = new List<FilterTemplate>();
                //foreach (var f in filter)
                //{
                //    filtertemplates.Add(JsonConvert.DeserializeObject<FilterTemplate>(f.Template));
                //}

                //var str = string.Join(", ", filtertemplates.Select(c => c.Cities));

                result = filtertemplates.Select(d => new FilterItemViewModel
                {
                    Cities = string.Join(", ", d.Cities),
                    DataTimeRangeFinish = d.DataTimeRangeFinish,
                    DataTimeRangeStart = d.DataTimeRangeStart,
                    DateTimeRange = d.DateTimeRange,
                    Include = d.Include,
                    IsMobile = d.IsMobile,
                    IsStatic = d.IsStatic,
                    Streets = string.Join(", ", d.Streets)
                });

                return Json(result.ToDataSourceResult(request));
            }
            return Json(null);
        }

        [AcceptVerbs("Post")]
        public ActionResult SaveFiltersTemplate([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<FilterTemplate> template)
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
        public IActionResult Create([Bind("id")] FilterItemViewModel filter)
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
                var filterTemplateListItem = new List<FilterTemplate>();
                if (HttpContext.Session.GetString(filter.Name) != null)
                {
                    filterTemplateListItem = HttpContext.Session.Get<List<FilterTemplate>>(filter.Name);
                    filterTemplateListItem.Add(filterTemplate);
                }
                else
                {
                    filterTemplateListItem.Add(filterTemplate);
                    HttpContext.Session.Set("filterName", filter.Name);
                }

                HttpContext.Session.Set(filter.Name, filterTemplateListItem);
                filter.Template = HttpContext.Session.GetString(filter.Name);

                var fn = HttpContext.Session.Get<string>("filterName");

                if (db.Filter.Select(arg => arg.Name).Any(f => f == fn))
                {
                    filter.id = db.Filter.SingleOrDefault(c => c.Name == fn).id;
                    db.Filter.Update(filter);
                }
                else
                {
                    db.Filter.Add(filter);
                }


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
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) :
                                  JsonConvert.DeserializeObject<T>(value);
        }
    }
}
