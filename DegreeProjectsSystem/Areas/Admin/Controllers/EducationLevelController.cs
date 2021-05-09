using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationLevelController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;

        public EducationLevelController(IUnitWork unitWork, INotyfService notyfService)
        {
            _unitWork = unitWork;
            _notyfService = notyfService;
        }

        enum Action
        {
            Create,
            Update,
            None
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateEducationLevel(int? id)
        {
            EducationLevel educationLevel = new EducationLevel();
            if (id == null)
            {
                educationLevel.Active = true;
                // Crea un nuevo registro
                return View(educationLevel);
            }
            // Actualiza el registro
            educationLevel = _unitWork.EducationLevel.Get(id.GetValueOrDefault());
            if (educationLevel == null)
            {
                return NotFound();
            }
            return View(educationLevel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateEducationLevel(EducationLevel educationLevel)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (educationLevel.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.EducationLevel.Add(educationLevel);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.EducationLevel.Update(educationLevel);
                }
                try
                {
                    _unitWork.Save();

                    if (action == Action.Create)
                    {
                        _notyfService.Success("Nível Educativo creado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Nível Educativo actualizado correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("IX_EducationLevels_Name"))
                    {
                        _notyfService.Error("Ya existe un Nível Educativo con el mismo nombre.");
                        
                        return View(educationLevel);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(educationLevel);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllEducationLevels()
        {
            var educationLevels = _unitWork.EducationLevel.GetAll();
            return Json(new { data = educationLevels });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteEducationLevel(int id)
        {
            // Actualiza el registro
            var educationLevelDb = _unitWork.EducationLevel.Get(id);

            if (educationLevelDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar nível educativo!! " });
            }

            educationLevelDb.Active = false;
            _unitWork.EducationLevel.Update(educationLevelDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Nível educativo borrado exitosamente" });

        }


        #endregion
    }
}
