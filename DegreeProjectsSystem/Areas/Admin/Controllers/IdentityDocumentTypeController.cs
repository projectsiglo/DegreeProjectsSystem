using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IdentityDocumentTypeController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;

        public IdentityDocumentTypeController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateIdentityDocumentType(int? id)
        {
            IdentityDocumentType identityDocumentType = new IdentityDocumentType();
            if ( id == null)
            {
                identityDocumentType.Active = true;
                // Crea un nuevo registro
                return View(identityDocumentType);
            }
            // Actualiza el registro
            identityDocumentType = _unitWork.IdentityDocumentType.Get(id.GetValueOrDefault());
            if (identityDocumentType == null)
            {
                return NotFound();
            }
            return View(identityDocumentType);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateIdentityDocumentType(IdentityDocumentType identityDocumentType)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (identityDocumentType.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.IdentityDocumentType.Add(identityDocumentType);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.IdentityDocumentType.Update(identityDocumentType);
                }
                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Tipo de documento de identidad creado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Tipo de documento de identidad actualizado correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_IdentityDocumentTypes_Name"))
                    {
                        _notyfService.Error("Ya existe un Tipo de Documento de Identidad con el mismo nombre.");
                        return View(identityDocumentType);
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
            return View(identityDocumentType);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllIdentityDocumentTypes() 
        {
            var identityDocumentTypes = _unitWork.IdentityDocumentType.GetAll();
            return Json(new { data = identityDocumentTypes });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteIdentityDocumentType(int id)
        {
            
            // Actualiza el registro
            var identityDocumentTypeDb = _unitWork.IdentityDocumentType.Get(id);

            if (identityDocumentTypeDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar Tipo de Documento de Identidad!! " });
            }

            identityDocumentTypeDb.Active = false;
            _unitWork.IdentityDocumentType.Update(identityDocumentTypeDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Tipo de Documento de Identidad borrado exitosamente" });

        }


        #endregion
    }
}
