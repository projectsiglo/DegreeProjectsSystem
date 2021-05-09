using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstitutionContactChargeController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;

        public InstitutionContactChargeController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateInstitutionContactCharge(int? id)
        {
            InstitutionContactCharge institutionContactCharge = new InstitutionContactCharge();
            if ( id == null)
            {
                institutionContactCharge.Active = true;
                // Crea un nuevo registro
                return View(institutionContactCharge);
            }
            // Actualiza el registro
            institutionContactCharge = _unitWork.InstitutionContactCharge.Get(id.GetValueOrDefault());
            if (institutionContactCharge == null)
            {
                return NotFound();
            }
            return View(institutionContactCharge);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateInstitutionContactCharge(InstitutionContactCharge institutionContactCharge)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (institutionContactCharge.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.InstitutionContactCharge.Add(institutionContactCharge);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.InstitutionContactCharge.Update(institutionContactCharge);
                }
                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Cargo contacto institución creado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Cargo contacto institución actualizado correctamente.");
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_InstitutionContactCharges_Name"))
                    {
                        _notyfService.Error("Ya existe un cargo contacto institución con el mismo nombre.");

                        return View(institutionContactCharge);
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
            return View(institutionContactCharge);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllInstitutionContactCharges() 
        {
            var institutionContactCharges = _unitWork.InstitutionContactCharge.GetAll();
            return Json(new { data = institutionContactCharges });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteInstitutionContactCharge(int id)
        {
            // Actualiza el registro
            var institutionContactChargeDb = _unitWork.InstitutionContactCharge.Get(id);

            if (institutionContactChargeDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar cargo contacto institución!!. " });
            }

            institutionContactChargeDb.Active = false;
            _unitWork.InstitutionContactCharge.Update(institutionContactChargeDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Cargo contacto institución borrado exitosamente." });

        }


        #endregion
    }
}
