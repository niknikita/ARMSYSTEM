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
    public class ProjectsController : Controller
    {
        private readonly PhonesContext db;
        private int idProject;

        public ProjectsController(PhonesContext context)
        {
            db = context;
        }

        // GET: Projects
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProjectDataRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(db.Projects.ToDataSourceResult(request));
        }


        public IActionResult BasesProjectDataRead([DataSourceRequest] DataSourceRequest request, [Bind("id", "Name")] Projects Projectread)
        {
            return Json(db.BasesProject.Where(c => c.idProject == Projectread.Id).ToDataSourceResult(request));
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await db.Projects.SingleOrDefaultAsync(m => m.Id == id);
            idProject = projects.Id;
            if (projects == null)
            {
                return NotFound();
            }
            ViewBag.BasesProject = db.BasesProject.Where(c => c.idProject == id).Select(c => c.Name).ToList();
            ViewBag.PhonesCount = db.PhoneBases.Where(p => p.idProject == id).Count();
            return View(projects);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Descripion,DateCreate")] Projects projectsToCreate)
        {
            if (projectsToCreate == null)
            {
                throw new ArgumentNullException(nameof(projectsToCreate));
            }

            if (ModelState.IsValid)
            {
                if (db.Projects.Any(p => p.Name == projectsToCreate.Name) == false)
                {
                    db.Add(projectsToCreate);
                    projectsToCreate.DateCreate = DateTime.Now;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(projectsToCreate);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await db.Projects.SingleOrDefaultAsync(m => m.Id == id);
            if (projects == null)
            {
                return NotFound();
            }
            return View(projects);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Descripion,DateCreate")] Projects projectToEdit)
        {
            if (id != projectToEdit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(projectToEdit);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectsExists(projectToEdit.Id))
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
            return View(projectToEdit);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await db.Projects
                .SingleOrDefaultAsync(m => m.Id == id);
            if (projects == null)
            {
                return NotFound();
            }

            return View(projects);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projects = await db.Projects.SingleOrDefaultAsync(m => m.Id == id);
            db.Projects.Remove(projects);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectsExists(int id)
        {
            return db.Projects.Any(e => e.Id == id);
        }
    }
}
