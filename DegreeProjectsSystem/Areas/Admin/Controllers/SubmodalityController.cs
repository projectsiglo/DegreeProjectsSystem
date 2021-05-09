using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubmodalityController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;

        public SubmodalityController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateSubmodality(int? id)
        {
            Submodality submodality = new Submodality();
            if (id == null)
            {
                submodality.Active = true;
                // Crea un nuevo registro
                return View(submodality);
            }
            // Actualiza el registro
            //institutionType = _unitWork
            submodality = _unitWork.Submodality.Get(id.GetValueOrDefault());
            if (submodality == null)
            {
                return NotFound();
            }
            return View(submodality);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateSubmodality(Submodality submodality)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (submodality.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.Submodality.Add(submodality);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.Submodality.Update(submodality);
                }

                try
                {
                    _unitWork.Save();

                    if (action == Action.Create)
                    {
                        _notyfService.Success("Submodalidad creada correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Submodalidad actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("IX_Submodalities_Name"))
                    {
                        _notyfService.Error("Ya existe una submodalidad con el mismo nombre.");

                        return View(submodality);
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

            return View(submodality);
        }
      

        #region API
        [HttpGet]
        public IActionResult GetAllSubmodalities()
        {
            var subbmodalities = _unitWork.Submodality.GetAll();
            return Json(new { data = subbmodalities });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteSubmodality(int id)
        {
            // Actualiza el registro
            var submodalityDb = _unitWork.Submodality.Get(id);

            if (submodalityDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar submodalidad!! " });
            }

            submodalityDb.Active = false;
            _unitWork.Submodality.Update(submodalityDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Submodalidad borrada exitosamente" });

        }


        #endregion
    }
}
