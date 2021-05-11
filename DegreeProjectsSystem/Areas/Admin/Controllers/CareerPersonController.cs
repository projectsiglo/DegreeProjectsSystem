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
    public class CareerPersonController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;
        private readonly ApplicationDbContext _db;

        public CareerPersonController(IUnitWork unitWork, INotyfService notyfService, ApplicationDbContext db)
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

        public IActionResult InsertOrUpdateCareerPerson(int? id)
        {
            CareerPersonViewModel careerPersonViewModel = new CareerPersonViewModel()
            {
                CareerPerson = new CareerPerson(),

                CareerList = _unitWork.Career.GetAll(orderBy: cpe => cpe.OrderBy(idt => idt.Name)).Select(cpe => new SelectListItem
                {
                    Text = cpe.Name,
                    Value = cpe.Id.ToString()
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
                careerPersonViewModel.CareerPerson.Active = true;
                return View(careerPersonViewModel);
            }

            // Actualiza el registro
            careerPersonViewModel.CareerPerson  = _unitWork.CareerPerson .Get(id.GetValueOrDefault());
            if (careerPersonViewModel.CareerPerson  == null)
            {
                return NotFound();
            }
            return View(careerPersonViewModel);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateCareerPerson(CareerPersonViewModel careerPersonViewModel)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (careerPersonViewModel.CareerPerson.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.CareerPerson.Add(careerPersonViewModel.CareerPerson);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.CareerPerson.Update(careerPersonViewModel.CareerPerson);
                }

                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Programa Persona creado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Programa Persona actualizado correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_CareerPeople_CareerId_PersonId"))
                    {

                        _notyfService.Error("Ya existe una Persona con el mismo programa.");

                        careerPersonViewModel.CareerList = _unitWork.Career.GetAll().Select(ca => new SelectListItem
                        {
                            Text = ca.Name,
                            Value = ca.Id.ToString()
                        });

                        careerPersonViewModel.PersonList = _unitWork.Person.GetAll().Select(pe => new SelectListItem
                        {
                            Text = pe.Names + " " + pe.Surnames,
                            Value = pe.Id.ToString()
                        }).ToList();

                        return View(careerPersonViewModel);
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
                careerPersonViewModel.CareerList = _unitWork.Career.GetAll().Select(ca => new SelectListItem
                {
                    Text = ca.Name,
                    Value = ca.Id.ToString()
                });

                careerPersonViewModel.PersonList = _unitWork.Person.GetAll(orderBy: pe => pe.OrderBy(pe => pe.Surnames + pe.Names)).Select(pe => new SelectListItem
                {
                    Text = pe.Names + " " + pe.Surnames,
                    Value = pe.Id.ToString()
                });

                if (careerPersonViewModel.CareerPerson.Id!=0)
                {
                    careerPersonViewModel.CareerPerson = _unitWork.CareerPerson.Get(careerPersonViewModel.CareerPerson.Id);
                }
             }
            return View(careerPersonViewModel);

        }

        #region API

        [HttpGet]
        public IActionResult GetAllCareerPeople()
        {
            var careerPeople = _unitWork.CareerPerson.GetAll(includeProperties: "Career,Person");
            return Json(new { data = careerPeople });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteCareerPerson(int id)
        {
            // Actualiza el registro
            var careerPersonDb = _unitWork.CareerPerson.Get(id);

            if (careerPersonDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar programa asignado a una Persona!!." });
            }

            careerPersonDb.Active = false;
            _unitWork.CareerPerson.Update(careerPersonDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Programa asignado a un a Persona borrado exitosamente." });

        }

        #endregion
    }
}