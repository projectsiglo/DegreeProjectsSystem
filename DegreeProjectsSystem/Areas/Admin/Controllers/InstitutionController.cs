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
    public class InstitutionController : Controller
    {
        private readonly IUnitWork _unitWork;

        private readonly INotyfService _notyfService;
        public InstitutionController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateInstitution(int? id)
        {
            InstitutionViewModel institutionViewModel = new InstitutionViewModel()
            {
                Institution = new Institution(),
                InstitutionTypeList = _unitWork.InstitutionType.GetAll().Select(it => new SelectListItem
                {
                    Text = it.Name,
                    Value = it.Id.ToString()
                })
            };

            if (id == null)
            {
                institutionViewModel.Institution.Active = true;
                // Crea un nuevo registro
                return View(institutionViewModel);
            }

            // Actualiza el registro
            institutionViewModel.Institution = _unitWork.Institution.Get(id.GetValueOrDefault());
            if (institutionViewModel.Institution == null)
            {
                return NotFound();
            }
            return View(institutionViewModel);

        }

        public IActionResult DetailInstitution(int? id)
        {
            InstitutionViewModel institutionViewModel = new InstitutionViewModel()
            {
                Institution = new Institution(),
                InstitutionTypeList = _unitWork.InstitutionType.GetAll().Select(it => new SelectListItem
                {
                    Text = it.Name,
                    Value = it.Id.ToString()
                })
            };

            institutionViewModel.Institution = _unitWork.Institution.Get(id.GetValueOrDefault());
            if (institutionViewModel.Institution == null)
            {
                return NotFound();
            }

            return View(institutionViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateInstitution(InstitutionViewModel institutionViewModel)
        {

            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (institutionViewModel.Institution.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.Institution.Add(institutionViewModel.Institution);
                }
                else
                {
                    action = Action.Update;
                    institutionViewModel.Institution.Email = institutionViewModel.Institution.Email.ToLower();
                    _unitWork.Institution.Update(institutionViewModel.Institution);
                }

                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Institución creada correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Institución actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_Institutions_Name"))
                    {

                        _notyfService.Error("Ya existe un Instución con el mismo nombre.");

                        institutionViewModel.InstitutionTypeList = _unitWork.InstitutionType.GetAll().Select(it => new SelectListItem
                        {
                            Text = it.Name,
                            Value = it.Id.ToString()
                        });

                        return View(institutionViewModel);
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
                institutionViewModel.InstitutionTypeList = _unitWork.InstitutionType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                if (institutionViewModel.Institution.Id != 0)
                {
                    institutionViewModel.Institution = _unitWork.Institution.Get(institutionViewModel.Institution.Id);
                }
            }
            return View(institutionViewModel);

        }

        #region API
        [HttpGet]
        public IActionResult GetAllInstitutions()
        {
            var institutions = _unitWork.Institution.GetAll(includeProperties: "InstitutionType");
            return Json(new { data = institutions });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteInstitution(int id)
        {
            // Actualiza el registro
            var institutionDb = _unitWork.Institution.Get(id);

            if (institutionDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar institución!!." });
            }

            institutionDb.Active = false;
            _unitWork.Institution.Update(institutionDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Institución borrada exitosamente." });

        }

        #endregion
    }
}