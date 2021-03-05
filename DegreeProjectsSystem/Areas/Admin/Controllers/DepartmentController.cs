using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly IUnitWork _unitWork;

        public DepartmentController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateDepartment(int? id)
        {
            Department department = new Department();
            if ( id == null)
            {
                // Crea un nuevo registro
                return View(department);
            }
            // Actualiza el registro
            department = _unitWork.Department.Get(id.GetValueOrDefault());
            if (department == null)
            {
                return NotFound();
            }
            return View(department);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                if (department.Id == 0)
                {
                    _unitWork.Department.Add(department);
                }
                else
                {
                    _unitWork.Department.Update(department);
                }
                _unitWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllDepartments() 
        {
            var departments = _unitWork.Department.GetAll();
            return Json(new { data = departments });
        }

        [HttpDelete]
        public IActionResult DeleteDepartment(int id) 
        {
            var departmentDb = _unitWork.Department.Get(id);

            if (departmentDb == null)
            {
                return Json(new { succes = false, mesage = "!!Error al borrar Departamento!! " });
            }
            _unitWork.Department.Remove(departmentDb);
            _unitWork.Save();
            return Json(new { succes = true, mesage = "Departamento borrado exitosamente" });
        }
        

        #endregion
    }
}
