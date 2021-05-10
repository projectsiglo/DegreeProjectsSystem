using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FacultyController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;

        public FacultyController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateFaculty(int? id)
        {
            Faculty faculty = new Faculty();
            if (id == null)
            {
                faculty.Active = true;
                // Crea un nuevo registro
                return View(faculty);
            }
            // Actualiza el registro
            faculty = _unitWork.Faculty.Get(id.GetValueOrDefault());
            if (faculty == null)
            {
                return NotFound();
            }
            return View(faculty);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateFaculty(Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (faculty.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.Faculty.Add(faculty);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.Faculty.Update(faculty);
                }

                try
                {
                    _unitWork.Save();

                    if (action == Action.Create)
                    {
                        _notyfService.Success("Facultad creada correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Facultad actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("IX_Faculties_Name"))
                    {
                        _notyfService.Error("Ya existe una Facultad con el mismo nombre.");
                        return View(faculty);
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
            return View(faculty);

        }

        #region API
        [HttpGet]
        public IActionResult GetAllFaculties()
        {
            var faculties = _unitWork.Faculty.GetAll();
            return Json(new { data = faculties });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteFaculty(int id)
        {
            // Actualiza el registro
            var facultytDb = _unitWork.Faculty.Get(id);

            if (facultytDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar Facultad!! " });
            }

            facultytDb.Active = false;
            _unitWork.Faculty.Update(facultytDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Facultad borrada exitosamente" });

        }

        #endregion
    }
}
