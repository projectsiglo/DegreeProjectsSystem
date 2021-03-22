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
    public class DepartmentFacultyController : Controller
    {
        private readonly IUnitWork _unitWork;
        public INotyfService _notyfService { get; }

        public DepartmentFacultyController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateDepartmentFaculty(int? id)
        {
            DepartmentFacultyViewModel departmentFacultyViewModel = new DepartmentFacultyViewModel()
            {
                DepartmentFaculty = new DepartmentFaculty(),
                FacultyList = _unitWork.Faculty.GetAll().Select(f => new SelectListItem
                {
                    Text = f.Name,
                    Value = f.Id.ToString()
                })
            };

            if (id == null)
            {
                departmentFacultyViewModel.DepartmentFaculty.Active = true;
                // Crea un nuevo registro
                return View(departmentFacultyViewModel);
            }

            // Actualiza el registro
            departmentFacultyViewModel.DepartmentFaculty = _unitWork.DepartmentFaculty.Get(id.GetValueOrDefault());
            if (departmentFacultyViewModel == null)
            {
                return NotFound();
            }
            return View(departmentFacultyViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateDepartmentFaculty(DepartmentFacultyViewModel departmentFacultyViewModel)
        {
            if (ModelState.IsValid)
            {
                Action action = Action.None;
                if (departmentFacultyViewModel.DepartmentFaculty.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.DepartmentFaculty.Add(departmentFacultyViewModel.DepartmentFaculty);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.DepartmentFaculty.Update(departmentFacultyViewModel.DepartmentFaculty);
                }

                try
                {
                    _unitWork.Save();
                    if (action == Action.Create)
                    {
                        _notyfService.Success("Departamento de facultad creado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Dewpartamento de facultad actualizado correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_DepartmentFaculties_Name"))
                    {

                        _notyfService.Error("Ya existe un departamento de facultad con el mismo nombre.");

                        departmentFacultyViewModel.FacultyList = _unitWork.Faculty.GetAll().Select(f => new SelectListItem
                        {
                            Text = f.Name,
                            Value = f.Id.ToString()
                        });

                        return View(departmentFacultyViewModel);
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
                departmentFacultyViewModel.FacultyList = _unitWork.Faculty.GetAll().Select(f => new SelectListItem
                {
                    Text = f.Name,
                    Value = f.Id.ToString()
                });

                if (departmentFacultyViewModel.DepartmentFaculty.Id != 0)
                {
                    departmentFacultyViewModel.DepartmentFaculty = _unitWork.DepartmentFaculty.Get(departmentFacultyViewModel.DepartmentFaculty.Id);
                }
            }

            return View(departmentFacultyViewModel);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllDepartmentFaculties()
        {
            var departmentFaculties = _unitWork.DepartmentFaculty.GetAll(includeProperties: "Faculty");
            return Json(new { data = departmentFaculties });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteDepartmentFaculty(int id)
        {
            DepartmentFaculty departmentFacultyDb = new DepartmentFaculty();
            // Actualiza el registro
            departmentFacultyDb = _unitWork.DepartmentFaculty.Get(id);

            if (departmentFacultyDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar departamento de facultad!! " });
            }

            departmentFacultyDb.Active = false;
            _unitWork.DepartmentFaculty.Update(departmentFacultyDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Departamento de facultad borrado exitosamente" });

        }


        #endregion
    }
}
