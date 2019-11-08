using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UturuAbiaNews.Controllers
{
    public class UturuAdminController : Controller
    {
        // GET: UturuAdmin
        public ActionResult Index()
        {
            return View();
        }

        // GET: UturuAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UturuAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UturuAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UturuAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UturuAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UturuAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UturuAdmin/Delete/5
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