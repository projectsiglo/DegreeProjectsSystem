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
    public class CityController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;

        public CityController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateCity(int? id)
        {
            CityViewModel cityViewModel = new CityViewModel()
            {
                City = new City(),
                DepartmentList = _unitWork.Department.GetAll().Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                })
            };

            if (id == null)
            {
                cityViewModel.City.Active = true;
                // Crea un nuevo registro
                return View(cityViewModel);
            }

            // Actualiza el registro
            cityViewModel.City = _unitWork.City.Get(id.GetValueOrDefault());
            if (cityViewModel.City == null)
            {
                return NotFound();
            }
            return View(cityViewModel);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateCity(CityViewModel cityViewModel)
        {
            
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (cityViewModel.City.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.City.Add(cityViewModel.City);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.City.Update(cityViewModel.City);
                }

                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Ciudad creada correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Ciudad actualizada correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_Cities_Name_DepartmentId"))
                    {

                        _notyfService.Error("Ya existe un Ciudad con el mismo nombre.");

                        cityViewModel.DepartmentList = _unitWork.Department.GetAll().Select(d => new SelectListItem
                        {
                            Text = d.Name,
                            Value = d.Id.ToString()
                        });

                        return View(cityViewModel);
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
                cityViewModel.DepartmentList = _unitWork.Department.GetAll().Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                });

                if (cityViewModel.City.Id!=0)
                {
                    cityViewModel.City = _unitWork.City.Get(cityViewModel.City.Id);
                }
             }
            return View(cityViewModel);

        }

        #region API
        [HttpGet]
        public IActionResult GetAllCities()
        {
            var cities = _unitWork.City.GetAll(includeProperties: "Department");
            return Json(new { data = cities });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteCity(int id)
        {
            // Actualiza el registro
            var cityDb = _unitWork.City.Get(id);

            if (cityDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar ciudad!!." });
            }

            cityDb.Active = false;
            _unitWork.City.Update(cityDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Ciudad borrada exitosamente." });

        }


        #endregion
    }
}