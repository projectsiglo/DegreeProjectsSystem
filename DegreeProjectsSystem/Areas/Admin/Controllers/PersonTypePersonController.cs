using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using DegreeProjectsSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonTypePersonController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;
        private readonly ApplicationDbContext _db;

        public PersonTypePersonController(IUnitWork unitWork, INotyfService notyfService, ApplicationDbContext db)
        {
            _unitWork = unitWork;
            _notyfService = notyfService;
            _db = db;
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

        public IActionResult InsertOrUpdatePersonTypePerson(int? id)
        {
            PersonTypePersonViewModel personTypePersonViewModel = new PersonTypePersonViewModel()
            {
                PersonTypePerson = new PersonTypePerson(),

                TypePersonList = _unitWork.TypePerson.GetAll(orderBy: tpe => tpe.OrderBy(tpe => tpe.Name)).Select(tpe => new SelectListItem
                {
                    Text = tpe.Name,
                    Value = tpe.Id.ToString()
                }),

                PersonList = _unitWork.Person.GetAll(orderBy: pe => pe.OrderBy(pe => pe.Surnames + pe.Names)).Select(pe => new SelectListItem
                {
                    Text = pe.Names + " " + pe.Surnames,
                    Value = pe.Id.ToString()
                }).ToList(),
            };

            // Crea un nuevo registro
            if (id == null)
            {
                personTypePersonViewModel.PersonTypePerson.Active = true;
                return View(personTypePersonViewModel);
            }

            // Actualiza el registro
            personTypePersonViewModel.PersonTypePerson = _unitWork.PersonTypePerson.Get(id.GetValueOrDefault());
            if (personTypePersonViewModel.PersonTypePerson == null)
            {
                return NotFound();
            }
            return View(personTypePersonViewModel);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdatePersonTypePerson(PersonTypePersonViewModel personTypePersonViewModel)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (personTypePersonViewModel.PersonTypePerson.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.PersonTypePerson.Add(personTypePersonViewModel.PersonTypePerson);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.PersonTypePerson.Update(personTypePersonViewModel.PersonTypePerson);
                }

                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Asignación Tipo de  Persona creaada correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Asignación Tipo de Persona actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_PersonTypePeople_PersonId_TypePersonId"))
                    {

                        _notyfService.Error("Ya existe una Persona con los mismos datos registrados");

                        personTypePersonViewModel.TypePersonList = _unitWork.TypePerson.GetAll().Select(tpe => new SelectListItem
                        {
                            Text = tpe.Name,
                            Value = tpe.Id.ToString()
                        });

                        personTypePersonViewModel.PersonList = _unitWork.Person.GetAll().Select(pe => new SelectListItem
                        {
                            Text = pe.Names + " " + pe.Surnames,
                            Value = pe.Id.ToString()
                        }).ToList();

                        return View(personTypePersonViewModel);
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
                personTypePersonViewModel.TypePersonList = _unitWork.TypePerson.GetAll().Select(tpe => new SelectListItem
                {
                    Text = tpe.Name,
                    Value = tpe.Id.ToString()
                });

                personTypePersonViewModel.PersonList = _unitWork.Person.GetAll(orderBy: pe => pe.OrderBy(pe => pe.Surnames + pe.Names)).Select(pe => new SelectListItem
                {
                    Text = pe.Names + " " + pe.Surnames,
                    Value = pe.Id.ToString()
                });

                if (personTypePersonViewModel.PersonTypePerson.Id != 0)
                {
                    personTypePersonViewModel.PersonTypePerson = _unitWork.PersonTypePerson.Get(personTypePersonViewModel.PersonTypePerson.Id);
                }
            }
            return View(personTypePersonViewModel);

        }

        #region API

        [HttpGet]
        public IActionResult GetAllPersonTypePeople()
        {
            var personTypePeople = _unitWork.PersonTypePerson.GetAll(includeProperties: "TypePerson,Person");
            return Json(new { data = personTypePeople });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeletePersonTypePerson(int id)
        {
            // Actualiza el registro
            var personTypePersonDb = _unitWork.PersonTypePerson.Get(id);

            if (personTypePersonDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar tipo de persona asignado!!." });
            }

            personTypePersonDb.Active = false;
            _unitWork.PersonTypePerson.Update(personTypePersonDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Tipo de persona asignado borrado exitosamente." });

        }

        #endregion
    }
}