using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstitutionTypeController : Controller
    {
        private readonly IUnitWork _unitWork;

        private readonly INotyfService _notyfService;

        public InstitutionTypeController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateInstitutionType(int? id)
        {
            InstitutionType institutionType = new InstitutionType();
            if (id == null)
            {
                institutionType.Active = true;
                // Crea un nuevo registro
                return View(institutionType);
            }
            // Actualiza el registro
            //institutionType = _unitWork
            institutionType = _unitWork.InstitutionType.Get(id.GetValueOrDefault());
            if (institutionType == null)
            {
                return NotFound();
            }
            return View(institutionType);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateInstitutionType(InstitutionType institutionType)
        {
            Action action = Action.None;
            if (ModelState.IsValid)
            {
                if (institutionType.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.InstitutionType.Add(institutionType);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.InstitutionType.Update(institutionType);
                }

                try
                {
                    _unitWork.Save();

                    if (action == Action.Create)
                    {
                        _notyfService.Success("Tipo de institución creada correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Tipo de institución actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("IX_InstitutionTypes_Name"))
                    {
                        _notyfService.Error("Ya existe un tipo de institución con el mismo nombre.");

                        return View(institutionType);
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

            return View(institutionType);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllInstitutionTypes()
        {
            var institutionTypes = _unitWork.InstitutionType.GetAll();
            return Json(new { data = institutionTypes });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteInstitutionType(int id)
        {
            // Actualiza el registro
            var institutionTypeDb = _unitWork.InstitutionType.Get(id);

            if (institutionTypeDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar tipo de institución!! " });
            }

            institutionTypeDb.Active = false;
            _unitWork.InstitutionType.Update(institutionTypeDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Tipo de institución borrado exitosamente" });

        }


        #endregion
    }
}
