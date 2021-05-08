using AspNetCoreHero.ToastNotification.Abstractions;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.DegreeProjectsSystem.DataAccess.Data;
using DegreeProjectsSystem.Models;
using DegreeProjectsSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;
        private readonly ApplicationDbContext _db;

        public PersonController(IUnitWork unitWork, INotyfService notyfService, ApplicationDbContext db)
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

        public IActionResult InsertOrUpdatePerson(int? id)
        {
            PersonViewModel personViewModel = new PersonViewModel()
            {
                Person = new Person(),

                IdentityDocumentTypeList = _unitWork.IdentityDocumentType.GetAll(orderBy: idt => idt.OrderBy(idt => idt.Name)).Select(idt => new SelectListItem
                {
                    Text = idt.Name,
                    Value = idt.Id.ToString()
                }),

                GenderList = _unitWork.Gender.GetAll(orderBy: ge => ge.OrderBy(ge => ge.Name)).Select(ge => new SelectListItem
                {
                    Text = ge.Name,
                    Value = ge.Id.ToString()
                }),


                DepartmentList = _unitWork.Department.GetAll(orderBy: de => de.OrderBy(de => de.Name)).Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                }),

                CityList = _unitWork.City.GetAll(orderBy: ci => ci.OrderBy(ci => ci.Name)).Select(ci => new SelectListItem
                {
                    Text = ci.Name,
                    Value = ci.Id.ToString()
                })
            };

            // Crea un nuevo registro
            if (id == null)
            {
                personViewModel.Person.Active = true;
                return View(personViewModel);
            }

            // Actualiza el registro
            personViewModel.Person  = _unitWork.Person .Get(id.GetValueOrDefault());
            if (personViewModel.Person  == null)
            {
                return NotFound();
            }
            return View(personViewModel);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdatePerson(PersonViewModel personViewModel)
        {
            personViewModel.Person.DepartmentId = Convert.ToInt32(Request.Form["DepartmentId"]);
            personViewModel.Person.CityId = Convert.ToInt32(Request.Form["CityId"]);

            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (personViewModel.Person.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.Person.Add(personViewModel.Person);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.Person.Update(personViewModel.Person);
                }

                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Persona creada correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Persona actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_People_IdentificationNumber"))
                    {

                        _notyfService.Error("Ya existe una Persona con el mismo número de identificacxión.");


                        personViewModel.IdentityDocumentTypeList = _unitWork.IdentityDocumentType.GetAll().Select(idt => new SelectListItem
                        {
                            Text = idt.Name,
                            Value = idt.Id.ToString()
                        });

                        personViewModel.GenderList = _unitWork.Gender.GetAll().Select(ge => new SelectListItem
                        {
                            Text = ge.Name,
                            Value = ge.Id.ToString()
                        });

                        personViewModel.DepartmentList = _unitWork.Department.GetAll().Select(d => new SelectListItem
                        {
                            Text = d.Name,
                            Value = d.Id.ToString()
                        });

                        personViewModel.CityList = _unitWork.City.GetAll().OrderBy(ci => ci.Name).Select(ci => new SelectListItem
                        {
                            Text = ci.Name,
                            Value = ci.Id.ToString()
                        });

                        return View(personViewModel);
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
                personViewModel.IdentityDocumentTypeList = _unitWork.IdentityDocumentType.GetAll().Select(idt => new SelectListItem
                {
                    Text = idt.Name,
                    Value = idt.Id.ToString()
                });

                personViewModel.GenderList = _unitWork.Gender.GetAll().Select(ge => new SelectListItem
                {
                    Text = ge.Name,
                    Value = ge.Id.ToString()
                });

                personViewModel.DepartmentList = _unitWork.Department.GetAll().Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                });

                personViewModel.CityList = _unitWork.City.GetAll().Select(ci => new SelectListItem
                {
                    Text = ci.Name,
                    Value = ci.Id.ToString()
                });

                if (personViewModel.Person.Id!=0)
                {
                    personViewModel.Person = _unitWork.Person.Get(personViewModel.Person.Id);
                }
             }
            return View(personViewModel);

        }

        //Details Person
        public IActionResult DetailPerson(int? id)
        {
            PersonViewModel personViewModel = new PersonViewModel()
            {
                IdentityDocumentTypeList = _unitWork.IdentityDocumentType.GetAll(orderBy: idt => idt.OrderBy(idt => idt.Name)).Select(idt => new SelectListItem
                {
                    Text = idt.Name,
                    Value = idt.Id.ToString()
                }),

                GenderList = _unitWork.Gender.GetAll(orderBy: ge => ge.OrderBy(ge => ge.Name)).Select(ge => new SelectListItem
                {
                    Text = ge.Name,
                    Value = ge.Id.ToString()
                }),


                DepartmentList = _unitWork.Department.GetAll(orderBy: de => de.OrderBy(de => de.Name)).Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                }),

                CityList = _unitWork.City.GetAll(orderBy: ci => ci.OrderBy(ci => ci.Name)).Select(ci => new SelectListItem
                {
                    Text = ci.Name,
                    Value = ci.Id.ToString()
                })
            
        };

            personViewModel.Person = _unitWork.Person.Get(id.GetValueOrDefault());
            if (personViewModel.Person == null)
            {
                return NotFound();
            }

            return View(personViewModel);
        }

        #region API

        [ActionName("GetCity")]
        public async Task<IActionResult> GetCity(int id)
        {
            List<City> cities = new List<City>();
            cities = await (from city in _db.Cities
                            where city.DepartmentId == id
                            orderby city.Name
                            select city).ToListAsync();
            return Json(new SelectList(cities, "Id", "Name"));
        }


        [HttpGet]
        public IActionResult GetAllPeople()
        {
            var people = _unitWork.Person.GetAll(includeProperties: "IdentityDocumentType,Gender,Department,City");
            return Json(new { data = people });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeletePerson(int id)
        {
            Person personDb = new Person();
            // Actualiza el registro
            personDb = _unitWork.Person.Get(id);

            if (personDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar persona!!." });
            }

            personDb.Active = false;
            _unitWork.Person.Update(personDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Persona borrada exitosamente." });

        }

        #endregion
    }
}