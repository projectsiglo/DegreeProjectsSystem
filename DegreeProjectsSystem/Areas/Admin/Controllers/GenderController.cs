using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenderController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;

        public GenderController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateGender(int? id)
        {
            Gender gender = new Gender();
            if ( id == null)
            {
                gender.Active = true;
                // Crea un nuevo registro
                return View(gender);
            }

            // Actualiza el registro
            gender = _unitWork.Gender.Get(id.GetValueOrDefault());
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateGender(Gender gender)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (gender.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.Gender.Add(gender);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.Gender.Update(gender);
                }
                try
                {
                    _unitWork.Save();
                   
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Género creado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Género actualizado correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_Genders_Name"))
                    {
                        _notyfService.Error("Ya existe un Género con el mismo nombre.");
                        
                        return View(gender);
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

            return View(gender);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllGenders()
        {
            var genders = _unitWork.Gender.GetAll();
            return Json(new { data = genders });
        }

        //Eliminación lógica de registro 
        [HttpPost]
        public IActionResult DeleteGender(int id)
        {
            // Actualiza el registro
            var genderDb = _unitWork.Gender.Get(id);

            if (genderDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar Género!!." });
            }

            genderDb.Active = false;
            _unitWork.Gender.Update(genderDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Género borrado exitosamente." });

        }


        #endregion
    }
}
