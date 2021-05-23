using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrograWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPrograWeb.Controllers
{
    public class PetsController : Controller
    {
        private readonly ProyectoPrograWebContext _dbcontext;

        public PetsController(ProyectoPrograWebContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // GET: MascotasController
        public ActionResult AdoptRequirements()
        {
            return View();
        }

        public ActionResult Index()
        {
            var Pets = _dbcontext.VPets;
            ViewBag.species = _dbcontext.Species;

            return View(Pets);
        }

        // GET: MascotasController/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: MascotasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MascotasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MascotasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MascotasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MascotasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MascotasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
