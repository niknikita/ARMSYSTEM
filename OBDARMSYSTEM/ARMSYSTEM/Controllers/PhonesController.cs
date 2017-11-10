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
using System.Data.SqlClient;
using ARMSYSTEM.Services.FiltersFizPhone;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.IO;

namespace ARMSYSTEM.Controllers
{
    public class PhonesController : Controller
    {
        private PhonesContext db;        
        private IHostingEnvironment HostingEnvironment { get; set; }
        public PhonesController(PhonesContext context, IHostingEnvironment hostingEnvironment)
        {
            db = context;
            HostingEnvironment = hostingEnvironment;
        }


        // GET: Phones
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PhoneModelRead([DataSourceRequest] DataSourceRequest request)
        {
            var result = db.Phones.FromSql("SELECT A.* FROM [arm2].[dbo].[Phones] as A LEFT JOIN [arm2].dbo.BlackList as B ON B.phone = A.Phone WHERE B.phone IS NULL");

            return Json(result.ToDataSourceResult(request));
            //return Json(db.Phones.ToDataSourceResult(request));
        }        


        // GET: Phones/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var phone = await db.Phones.SingleOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }
            var PhoneStatCount = db.PhoneBases.Where(p => p.Phone == phone.Phone).Count();
            SqlParameter param = new SqlParameter("@phone", phone.Phone);
            var BasesList = db.BasesProject.FromSql("SELECT Phones.Phone, BasesProject.Name FROM dbo.Phones INNER JOIN dbo.PhoneBases ON Phones.Phone = PhoneBases.Phone INNER JOIN dbo.BasesProject ON PhoneBases.idBase = BasesProject.id WHERE Phones.Phone = @phone", param).Select(c=>c.Name).ToList();
            var ProjectList = db.BasesProject.FromSql("SELECT Phones.Phone, Projects.Name FROM dbo.Phones INNER JOIN dbo.PhoneBases ON Phones.Phone = PhoneBases.Phone INNER JOIN dbo.Projects ON PhoneBases.idProject = Projects.id WHERE Phones.Phone = @phone", param).Select(c => c.Name).ToList();
            ViewBag.PhoneBasesCount = PhoneStatCount;
            ViewBag.PhoneBasesList = BasesList;
            ViewBag.PhoneProjectList = ProjectList;
            return View(phone);
        }

        // GET: Phones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Phones/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Phones phoneToCreate)
        {
            if (phoneToCreate == null)
            {
                throw new ArgumentNullException(nameof(phoneToCreate));
            }

            try
            {
                if (db.Phones.Any(p => p.Phone != phoneToCreate.Phone))
                {
                    if (db.BlackList.Any(p => p.Phone == phoneToCreate.Phone) == false)
                    {
                        phoneToCreate.DateUpdate = DateTime.Now;
                        db.Phones.Add(phoneToCreate);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Create");
                    }
                    else
                        return RedirectToAction("Index"); //вывод ошибки
                }
                else
                    return RedirectToAction("Edit"); //вывод ошибки

            }
            catch
            {
                return View();
            }
        }
        public ActionResult Save(IFormFile files)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                string path = "/App_Data/" + files.FileName;
                using (var fileStream = new FileStream(HostingEnvironment.WebRootPath + path, FileMode.Create))
                {
                    files.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = files.FileName, Path = path };
                db.File.Add(file);
                db.SaveChanges();
            }

            // Return an empty string to signify success
            return RedirectToAction("Create");
        }

        // GET: Phones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var phone = db.Phones.FirstOrDefault<Phones>(p => p.Id == id);
                if (phone != null)
                    return View(phone);
            }
            return NotFound();
        }

        // POST: Phones/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Phones phonesToEdit)
        {
            try
            {
                // TODO: Add update logic here
                if (phonesToEdit != null && ModelState.IsValid)
                {
                    db.Phones.Update(phonesToEdit);
                    db.SaveChanges();
                }
                return RedirectToAction("Edit/" + phonesToEdit.Id.ToString());
            }
            catch
            {
                return View();
            }
        }

        // GET: Phones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Phones/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}