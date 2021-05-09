using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TypePersonController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;

        public TypePersonController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateTypePerson(int? id)
        {
            TypePerson typePerson = new TypePerson();
            if (id == null)
            {
                typePerson.Active = true;
                // Crea un nuevo registro
                return View(typePerson);
            }
            // Actualiza el registro
            typePerson = _unitWork.TypePerson.Get(id.GetValueOrDefault());
            if (typePerson == null)
            {
                return NotFound();
            }
            return View(typePerson);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateTypePerson(TypePerson typePerson)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (typePerson.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.TypePerson.Add(typePerson);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.TypePerson.Update(typePerson);
                }

                try
                {
                    _unitWork.Save();

                    if (action == Action.Create)
                    {
                        _notyfService.Success("Tipo de persona creada correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Tipo de persona actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {

                    if (dbUpdateException.InnerException.Message.Contains("IX_TypePeople_Name"))
                    {
                        _notyfService.Error("Ya existe un tipo de persona con el mismo nombre.");

                        return View(typePerson);
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
               
            return View(typePerson);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllTypePeople()
        {
            var typePeople = _unitWork.TypePerson.GetAll();
            return Json(new { data = typePeople });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteTypePerson(int id)
        {
            // Actualiza el registro
            var typePersonDb = _unitWork.TypePerson.Get(id);

            if (typePersonDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar tipo de persona!! " });
            }

            typePersonDb.Active = false;
            _unitWork.TypePerson.Update(typePersonDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "tipo de persona borrada exitosamente" });

        }


        #endregion
    }
}
