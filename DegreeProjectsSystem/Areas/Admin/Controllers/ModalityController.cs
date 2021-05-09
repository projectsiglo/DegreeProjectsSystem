using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using DegreeProjectsSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ModalityController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;

        public ModalityController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateModality(int? id)
        {
            ModalityViewModel modalityViewModel = new ModalityViewModel()
            {
                Modality = new Modality(),
                EducationLevelList = _unitWork.EducationLevel.GetAll().Select(el => new SelectListItem
                {
                    Text = el.Name,
                    Value = el.Id.ToString()
                })
            };

            if (id == null)
            {
                modalityViewModel.Modality.Active = true;
                // Crea un nuevo registro
                return View(modalityViewModel);
            }

            // Actualiza el registro
            modalityViewModel.Modality = _unitWork.Modality.Get(id.GetValueOrDefault());
            if (modalityViewModel == null)
            {
                return NotFound();
            }
            return View(modalityViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateModality(ModalityViewModel modalityViewModel)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (modalityViewModel.Modality.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.Modality.Add(modalityViewModel.Modality);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.Modality.Update(modalityViewModel.Modality);
                }

                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Modalidad creada correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Modalidad actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_Modalities_Name"))
                    {

                        _notyfService.Error("Ya existe una modalidad con el mismo nombre.");

                        modalityViewModel.EducationLevelList = _unitWork.EducationLevel.GetAll().Select(el => new SelectListItem
                        {
                            Text = el.Name,
                            Value = el.Id.ToString()
                        });

                        return View(modalityViewModel);
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
            else
            {
                modalityViewModel.EducationLevelList = _unitWork.EducationLevel.GetAll().Select(el => new SelectListItem
                {
                    Text = el.Name,
                    Value = el.Id.ToString()
                });

                if (modalityViewModel.Modality.Id != 0)
                {
                    modalityViewModel.Modality = _unitWork.Modality.Get(modalityViewModel.Modality.Id);
                }
            }

            return View(modalityViewModel);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllModalities()
        {
            var modalities = _unitWork.Modality.GetAll(includeProperties: "EducationLevel");
            return Json(new { data = modalities });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteModality(int id)
        {
            // Actualiza el registro
            var modalityDb = _unitWork.Modality.Get(id);

            if (modalityDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar modalidad!! " });
            }

            modalityDb.Active = false;
            _unitWork.Modality.Update(modalityDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Modalidad borrada exitosamente." });
        }

        #endregion
    }
}
