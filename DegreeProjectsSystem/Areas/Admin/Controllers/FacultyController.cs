using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FacultyController : Controller
    {
        private readonly IUnitWork _unitWork;

        public FacultyController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateFaculty(int? id)
        {
            Faculty faculty = new Faculty();
            if ( id == null)
            {
                faculty.Active = true;
                // Crea un nuevo registro
                return View(faculty);
            }
            // Actualiza el registro
            faculty = _unitWork.Faculty.Get(id.GetValueOrDefault());
            if (faculty == null)
            {
                return NotFound();
            }
            return View(faculty);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateFaculty(Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                if (faculty.Id == 0)
                {
                    _unitWork.Faculty.Add(faculty);
                }
                else
                {
                    _unitWork.Faculty.Update(faculty);
                }
                _unitWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllFaculties() 
        {
            var faculties = _unitWork.Faculty.GetAll();
            return Json(new { data = faculties });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteFaculty(int id)
        {
            Faculty facultytDb = new Faculty();
            // Actualiza el registro
            facultytDb = _unitWork.Faculty.Get(id);

            if (facultytDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar Facultad!! " });
            }

            facultytDb.Active = false;
            _unitWork.Faculty.Update(facultytDb);
            _unitWork.Save();

            return Json(new { succes = true, message = "Facultad borrada exitosamente" });

        }


        #endregion
    }
}
