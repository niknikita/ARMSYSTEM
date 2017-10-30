using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ARMSYSTEM.Models;
using ARMSYSTEM.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.EntityFrameworkCore;


namespace ARMSYSTEM.Controllers
{
    public class BasesProjectsController : Controller
    {
        private readonly PhonesContext db;

        public BasesProjectsController(PhonesContext context)
        {
            db = context;
        }

        // GET: BasesProjects
        public async Task<IActionResult> Index()
        {
            var rezult = db.BasesProject.Join(db.Projects,
                b => b.idProject,
                p => p.Id,
                (b, p) => new BasesProjectViewModel
                {
                    id = b.id,
                    Name = b.Name,
                    ProjectName = p.Name,
                    idParrentBase = b.idParrentBase,
                    filters = b.filters,
                    description = b.description,
                    dateCreate = b.dateCreate
                });
            List<BasesProjectViewModel> list = new List<BasesProjectViewModel>() ;
            list.AddRange(rezult);
            return View(list);
        }
        public ActionResult BasesProjectRead([DataSourceRequest] DataSourceRequest request)
        {   
            return Json(db.BasesProject.ToDataSourceResult(request));
        }        
        // GET: BasesProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basesProject = await db.BasesProject
                .SingleOrDefaultAsync(m => m.id == id);
            if (basesProject == null)
            {
                return NotFound();
            }

            return View(basesProject);
        }

        // GET: BasesProjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BasesProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,idProject,idParrentBase,filters,description,dateCreate")] BasesProject basesProject)
        {
            if (ModelState.IsValid)
            {
                db.Add(basesProject);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(basesProject);
        }

        // GET: BasesProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basesProject = await db.BasesProject.SingleOrDefaultAsync(m => m.id == id);
            if (basesProject == null)
            {
                return NotFound();
            }
            return View(basesProject);
        }

        // POST: BasesProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,idProject,idParrentBase,filters,description,dateCreate")] BasesProject basesProject)
        {
            if (id != basesProject.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(basesProject);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasesProjectExists(basesProject.id))
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
            return View(basesProject);
        }

        // GET: BasesProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basesProject = await db.BasesProject
                .SingleOrDefaultAsync(m => m.id == id);
            if (basesProject == null)
            {
                return NotFound();
            }

            return View(basesProject);
        }

        // POST: BasesProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var basesProject = await db.BasesProject.SingleOrDefaultAsync(m => m.id == id);
            db.BasesProject.Remove(basesProject);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasesProjectExists(int id)
        {
            return db.BasesProject.Any(e => e.id == id);
        }
    }
}
