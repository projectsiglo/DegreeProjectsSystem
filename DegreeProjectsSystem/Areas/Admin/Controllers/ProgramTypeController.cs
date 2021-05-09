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
    public class ProgramTypeController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;

        public ProgramTypeController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateProgramType(int? id)
        {
            ProgramTypeViewModel programTypeViewModel = new ProgramTypeViewModel()
            {
                ProgramType = new ProgramType(),
                EducationLevelList = _unitWork.EducationLevel.GetAll().Select(el => new SelectListItem
                {
                    Text = el.Name,
                    Value = el.Id.ToString()
                })
            };

            if (id == null)
            {
                programTypeViewModel.ProgramType.Active = true;
                // Crea un nuevo registro
                return View(programTypeViewModel);
            }

            // Actualiza el registro
            programTypeViewModel.ProgramType = _unitWork.ProgramType.Get(id.GetValueOrDefault());
            if (programTypeViewModel == null)
            {
                return NotFound();
            }
            return View(programTypeViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateProgramType(ProgramTypeViewModel programTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (programTypeViewModel.ProgramType.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.ProgramType.Add(programTypeViewModel.ProgramType);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.ProgramType.Update(programTypeViewModel.ProgramType);
                }

                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Tipo de programa creado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Tipo de programa actualizado correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_ProgramTypes_Name"))
                    {

                        _notyfService.Error("Ya existe un tipo de programa con el mismo nombre.");

                        programTypeViewModel.EducationLevelList = _unitWork.EducationLevel.GetAll().Select(el => new SelectListItem
                        {
                            Text = el.Name,
                            Value = el.Id.ToString()
                        });

                        return View(programTypeViewModel);
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
                programTypeViewModel.EducationLevelList = _unitWork.EducationLevel.GetAll().Select(el => new SelectListItem
                {
                    Text = el.Name,
                    Value = el.Id.ToString()
                });

                if (programTypeViewModel.ProgramType.Id != 0)
                {
                    programTypeViewModel.ProgramType = _unitWork.ProgramType.Get(programTypeViewModel.ProgramType.Id);
                }
            }

            return View(programTypeViewModel);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllProgramTypes()
        {
            var programTypes = _unitWork.ProgramType.GetAll(includeProperties: "EducationLevel");
            return Json(new { data = programTypes });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteProgramType(int id)
        {
            // Actualiza el registro
            var programTypeDb = _unitWork.ProgramType.Get(id);

            if (programTypeDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar tipo de programa!! " });
            }

            programTypeDb.Active = false;
            _unitWork.ProgramType.Update(programTypeDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Tipo de programa borrado exitosamente" });
        }

        #endregion
    }
}
