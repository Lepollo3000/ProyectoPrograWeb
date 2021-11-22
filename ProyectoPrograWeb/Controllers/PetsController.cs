using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoPrograWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoPrograWeb.Controllers
{
    public class PetsController : Controller
    {
        private readonly ProyectoPrograWebContext _dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PetsController(ProyectoPrograWebContext dbcontext, IWebHostEnvironment hostEnvironment)
        {
            _dbcontext = dbcontext;
            _hostEnvironment = hostEnvironment;
        }

        // GET: MascotasController
        public ActionResult AdoptRequirements()
        {
            return View();
        }

        [Authorize]
        public ActionResult Index(int idSpecie)
        {
            var pets = _dbcontext.VPets
                .Where(p => p.IdSpeciePet == idSpecie);
            ViewBag.species = _dbcontext.Species;
            ViewBag.breed = _dbcontext.Breeds
                .Where(b => b.IdSpecieRace == idSpecie);
            ViewBag.idSpecie = idSpecie;

            return View(pets);
        }

        // GET: MascotasController/Details/5
        [Authorize]
        public ActionResult Details(int idPet)
        {
            var pet = _dbcontext.VPets
                .Where(p => p.IdPet == idPet).FirstOrDefault();
            ViewBag.breed = _dbcontext.Breeds
                .Where(b => b.IdSpecieRace == pet.IdSpeciePet);
            ViewBag.energyLevels = _dbcontext.EnergyLevels;

            return View(pet);
        }

        [Authorize]
        public ActionResult RequestDetails(int idRequest)
        {
            var request = _dbcontext.VAdoptionUsers
                .Where(r => r.IdAdoptionRequest == idRequest).FirstOrDefault();

            return View(request);
        }

        // GET: MascotasController/Create
        public ActionResult Create(int idSpecie)
        {
            var breed = _dbcontext.Breeds
                .Where(b => b.IdSpecieRace == idSpecie);
            var energyLevels = _dbcontext.EnergyLevels;
            var specie = _dbcontext.Species
                .Where(s => s.IdSpecie == idSpecie).FirstOrDefault().NameSpecie;

            ViewBag.specie = idSpecie;
            ViewBag.nameSpecie = specie;
            ViewBag.breed = new SelectList(breed, "IdBreed", "NameBreed");
            ViewBag.energyLevels = new SelectList(energyLevels, "LevelId", "LevelName");

            return View();
        }

        // POST: MascotasController/Create
        [HttpPost]
        public async Task<ActionResult> CreateAsync(IFormFile Photo, int IdSpeciePet, int IdBreedPet, int AgePet, bool IsAgeMonth, decimal WeightPet, int IdSexPet, string NamePet, int EnergyLevelId, string DescriptionPet)
        {
            try
            {
                string wwwRoothPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(Photo.FileName);
                string extension = Path.GetExtension(Photo.FileName);

                fileName = fileName + DateTime.Now.ToString("yyyyMMddmmss") + extension;

                string path = Path.Combine(wwwRoothPath + "/img/" + fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Photo.CopyToAsync(stream);
                }

                var pet = new Pet()
                {
                    IdSpeciePet = IdSpeciePet,
                    IdBreedPet = IdBreedPet,
                    AgePet = AgePet,
                    IsAgeMonth = IsAgeMonth,
                    WeightPet = WeightPet,
                    IdSexPet = IdSexPet,
                    NamePet = NamePet,
                    EnergyLevelId = EnergyLevelId,
                    DescriptionPet = DescriptionPet,
                    PhotoPathPet = "/img/" + fileName,
                    IdStatusPet = 1
                };

                _dbcontext.Pets.Add(pet);
                _dbcontext.SaveChanges();

                return RedirectToAction(nameof(Index), new { idSpecie = IdSpeciePet });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AdoptionConfirm()
        {
            string idUser = null;

            if (!(User.IsInRole("admin")))
            {
                ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
                idUser = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            if (idUser != null)
            {
                var adoptions = _dbcontext.VAdoptionUsers
                    .Where(a => a.IdUser == idUser);

                return View(adoptions);
            }
            else
            {
                var adoptions = _dbcontext.VAdoptionUsers;

                return View(adoptions);
            }
        }

        [HttpPost]
        public ActionResult AdoptRequest(int idPet, string cellphone, string whereWho, string reason)
        {
            var pet = _dbcontext.Pets
                    .Where(p => p.IdPet == idPet).FirstOrDefault();

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (pet != null)
            {
                if (userId != null)
                {
                    try
                    {
                        pet.IdStatusPet = 2;

                        _dbcontext.Pets.Update(pet);
                        _dbcontext.SaveChanges();

                        var adoptRequest = new AdoptionRequest()
                        {
                            CellphoneUser = cellphone,
                            ReasonAdoption = reason,
                            WhereWhoAdoption = whereWho,
                            IdUser = userId,
                            IdPet = idPet
                        };

                        _dbcontext.AdoptionRequests.Add(adoptRequest);
                        _dbcontext.SaveChanges();

                        //EXITO
                        Response.StatusCode = 200;
                        var mensaje = "Message sent. Please wait until we contact you.";

                        return new JsonResult(mensaje);
                    }
                    catch
                    {
                        //ERROR
                        Response.StatusCode = 500;
                        var error = "Internal server error. Try again later.";

                        return new JsonResult(error);
                    }
                }
                else
                {
                    //ERROR
                    Response.StatusCode = 404;
                    var error = "The user hasn't been found or doesn't exist.";

                    return new JsonResult(error);
                }
            }
            else
            {
                //ERROR
                Response.StatusCode = 404;
                var error = "This pet hasn't been found or doesn't exist.";

                return new JsonResult(error);
            }
        }

        [HttpPost]
        public IActionResult AcceptAdoption(int idAdoptionRequest)
        {
            var adoption = _dbcontext.AdoptionRequests
                .Where(a => a.IdAdoptionRequest == idAdoptionRequest).FirstOrDefault();

            if (adoption != null)
            {
                var pet = _dbcontext.Pets
                    .Where(p => p.IdPet == adoption.IdPet).FirstOrDefault();

                if (pet != null)
                {
                    _dbcontext.AdoptionRequests.Remove(adoption);
                    _dbcontext.SaveChanges();

                    _dbcontext.Pets.Remove(pet);
                    _dbcontext.SaveChanges();

                    string wwwRoothPath = _hostEnvironment.WebRootPath;
                    string pathOld = Path.Combine(wwwRoothPath + pet.PhotoPathPet);

                    System.IO.File.Delete(pathOld);

                    //EXITO
                    Response.StatusCode = 200;
                    var mensaje = "Adoption done succesfully.";

                    return new JsonResult(mensaje);
                }
                else
                {
                    //ERROR
                    Response.StatusCode = 404;
                    var error = "This pet hasn't been found or doesn't exist.";

                    return new JsonResult(error);
                }
            }
            else
            {
                //ERROR
                Response.StatusCode = 404;
                var error = "This request hasn't been found or doesn't exist.";

                return new JsonResult(error);
            }
        }

        [HttpPost]
        public IActionResult CancelAdoption(int idAdoptionRequest)
        {
            var adoption = _dbcontext.AdoptionRequests
                .Where(a => a.IdAdoptionRequest == idAdoptionRequest).FirstOrDefault();

            if (adoption != null)
            {
                var pet = _dbcontext.Pets
                    .Where(p => p.IdPet == adoption.IdPet).FirstOrDefault();

                if (pet != null)
                {
                    pet.IdStatusPet = 1;
                    _dbcontext.Pets.Update(pet);
                    _dbcontext.SaveChanges();

                    _dbcontext.AdoptionRequests.Remove(adoption);
                    _dbcontext.SaveChanges();

                    //EXITO
                    Response.StatusCode = 200;
                    var mensaje = "Adoption cancelled succesfully.";

                    return new JsonResult(mensaje);
                }
                else
                {
                    //ERROR
                    Response.StatusCode = 404;
                    var error = "This pet hasn't been found or doesn't exist.";

                    return new JsonResult(error);
                }
            }
            else
            {
                //ERROR
                Response.StatusCode = 404;
                var error = "This request hasn't been found or doesn't exist.";

                return new JsonResult(error);
            }
        }

        // POST: MascotasController/Edit/5
        [HttpPost]
        public async Task<ActionResult> EditAsync(IFormFile? Photo, int idPet, string namePet, int idsex, decimal weight, bool isMonth, int energyLevel, int age, int breed, string description)
        {
            var pet = _dbcontext.Pets
                    .Where(p => p.IdPet == idPet).FirstOrDefault();

            if (pet != null)
            {
                try
                {
                    if (Photo != null)
                    {
                        string wwwRoothPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(Photo.FileName);
                        string extension = Path.GetExtension(Photo.FileName);

                        fileName = fileName + DateTime.Now.ToString("yyyyMMddmmss") + extension;

                        string pathNew = Path.Combine(wwwRoothPath + "/img/" + fileName);
                        string pathOld = Path.Combine(wwwRoothPath + pet.PhotoPathPet);

                        using (var stream = new FileStream(pathNew, FileMode.Create))
                        {
                            await Photo.CopyToAsync(stream);
                        }

                        pet.PhotoPathPet = "/img/" + fileName;

                        System.IO.File.Delete(pathOld);
                    }

                    pet.IdSexPet = pet.IdSexPet != idsex ? idsex : pet.IdSexPet;
                    pet.NamePet = pet.NamePet != namePet ? namePet : pet.NamePet;
                    pet.WeightPet = pet.WeightPet != idsex ? weight : pet.WeightPet;
                    pet.IsAgeMonth = pet.IsAgeMonth != isMonth ? isMonth : pet.IsAgeMonth;
                    pet.EnergyLevelId = pet.EnergyLevelId != energyLevel ? energyLevel : pet.EnergyLevelId;
                    pet.AgePet = pet.AgePet != age ? age : pet.AgePet;
                    pet.IdBreedPet = pet.IdBreedPet != breed ? breed : pet.IdBreedPet;
                    pet.DescriptionPet = pet.DescriptionPet != description ? description : pet.DescriptionPet;

                    _dbcontext.Pets.Update(pet);
                    _dbcontext.SaveChanges();

                    //EXITO
                    Response.StatusCode = 200;

                    return RedirectToAction(nameof(Details), new { idPet = pet.IdPet });
                }
                catch
                {
                    //ERROR
                    Response.StatusCode = 500;
                    var error = "Internal server error. Try again later.";

                    return new JsonResult(error);
                }
            }
            else
            {
                //ERROR
                Response.StatusCode = 404;
                var error = "This pet hasn't been found or doesn't exist.";

                return new JsonResult(error);
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
