using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeachingFunctionController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;

        public TeachingFunctionController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateTeachingFunction(int? id)
        {
            TeachingFunction teachingFunction = new TeachingFunction();
            if (id == null)
            {
                teachingFunction.Active = true;
                // Crea un nuevo registro
                return View(teachingFunction);
            }
            // Actualiza el registro
            teachingFunction = _unitWork.TeachingFunction.Get(id.GetValueOrDefault());
            if (teachingFunction == null)
            {
                return NotFound();
            }
            return View(teachingFunction);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateTeachingFunction(TeachingFunction teachingFunction)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (teachingFunction.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.TeachingFunction.Add(teachingFunction);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.TeachingFunction.Update(teachingFunction);
                }
                try
                {
                    _unitWork.Save();

                    if (action == Action.Create)
                    {
                        _notyfService.Success("Función docente creada correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Función docente actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("IX_TeachingFunctions_Name"))
                    {
                        _notyfService.Error("Ya existe una función docente con el mismo nombre.");

                        return View(teachingFunction);
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

            return View(teachingFunction);

        }


        #region API
        [HttpGet]
        public IActionResult GetAllTeachingFunctions()
        {
            var teachingFunctions = _unitWork.TeachingFunction.GetAll();
            return Json(new { data = teachingFunctions });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteTeachingFunction(int id)
        {
            // Actualiza el registro
            var teachingFunctionDb = _unitWork.TeachingFunction.Get(id);

            if (teachingFunctionDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar función docente! " });
            }

            teachingFunctionDb.Active = false;
            _unitWork.TeachingFunction.Update(teachingFunctionDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Tipo de función docente borrada exitosamente" });

        }


        #endregion
    }
}
