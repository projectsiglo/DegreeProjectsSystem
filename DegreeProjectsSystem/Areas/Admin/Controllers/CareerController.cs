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
    public class CareerController : Controller
    {
        private readonly IUnitWork _unitWork;

        public INotyfService _notifyService { get; }


        public CareerController(IUnitWork unitWork, INotyfService notifyService)
        {
            _unitWork = unitWork;
            _notifyService = notifyService;
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

        public IActionResult InsertOrUpdateCareer(int? id)
        {
            CareerViewModel careerViewModel = new CareerViewModel()
            {
                Career = new Career(),
                ProgramTypeList = _unitWork.ProgramType.GetAll().Select(pt => new SelectListItem
                {
                    Text = pt.Name,
                    Value = pt.Id.ToString()
                })
            };

            if (id == null)
            {
                careerViewModel.Career.Active = true;
                // Crea un nuevo registro
                return View(careerViewModel);
            }

            // Actualiza el registro
            careerViewModel.Career = _unitWork.Career.Get(id.GetValueOrDefault());
            if (careerViewModel == null)
            {
                return NotFound();
            }
            return View(careerViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateCareer(CareerViewModel careerViewModel)
        {
            Action action = Action.None;
            if (ModelState.IsValid)
            {
                if (careerViewModel.Career.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.Career.Add(careerViewModel.Career);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.Career.Update(careerViewModel.Career);
                }

                try
                {
                    _unitWork.Save();

                    if (action == Action.Create)
                    {
                        _notifyService.Success("Programa creado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notifyService.Success("Programa actualizado correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_Careers_Name_ProgramTypeId"))
                    {
                        _notifyService.Warning("Ya existe un Programa con el mismo nombre.");

                        careerViewModel.ProgramTypeList = _unitWork.ProgramType.GetAll().Select(pt => new SelectListItem
                        {
                            Text = pt.Name,
                            Value = pt.Id.ToString()
                        });

                        return View(careerViewModel);
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
                careerViewModel.ProgramTypeList = _unitWork.ProgramType.GetAll().Select(pt => new SelectListItem
                {
                    Text = pt.Name,
                    Value = pt.Id.ToString()
                });

                if (careerViewModel.Career.Id != 0)
                {
                    careerViewModel.Career = _unitWork.Career.Get(careerViewModel.Career.Id);
                }
            }
            return View(careerViewModel);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllCareers()
        {
            var careers = _unitWork.Career.GetAll(includeProperties: "ProgramType");
            return Json(new { data = careers });
        }

        //Eliminación lógica de registro 
        [HttpPost]
        public IActionResult DeleteCareer(int id)
        {
            Career careerDb = new Career();
            // Actualiza el registro
            careerDb = _unitWork.Career.Get(id);

            if (careerDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar el programa!! " });
            }

            careerDb.Active = false;
            _unitWork.Career.Update(careerDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Programa borrado exitosamente" });

        }


        #endregion
    }
}
