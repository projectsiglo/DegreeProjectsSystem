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
                department.Active = true;
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

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteDepartment(int id)
        {
            Department departmentDb = new Department();
            // Actualiza el registro
            departmentDb = _unitWork.Department.Get(id);

            if (departmentDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar Departamento!! " });
            }

            departmentDb.Active = false;
            _unitWork.Department.Update(departmentDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Departamento borrado exitosamente" });

        }


        #endregion
    }
}
