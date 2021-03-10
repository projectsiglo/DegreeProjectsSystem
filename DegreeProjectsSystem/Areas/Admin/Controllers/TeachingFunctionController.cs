using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeachingFunctionController : Controller
    {
        private readonly IUnitWork _unitWork;

        public TeachingFunctionController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateTeachingFunction(int? id)
        {
            TeachingFunction teachingFunction = new TeachingFunction();
            if (id == null)
            {
                teachingFunction.Active = true;
                // Crea un nuevo registro
                return View(teachingFunction);
            }
            // Actualiza el registro
            teachingFunction = _unitWork.TeachingFunction.Get(id.GetValueOrDefault());
            if (teachingFunction == null)
            {
                return NotFound();
            }
            return View(teachingFunction);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateTeachingFunction(TeachingFunction teachingFunction)
        {
            if (ModelState.IsValid)
            {
                if (teachingFunction.Id == 0)
                {
                    _unitWork.TeachingFunction.Add(teachingFunction);
                }
                else
                {
                    _unitWork.TeachingFunction.Update(teachingFunction);
                }
                _unitWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(teachingFunction);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllTeachingFunctions()
        {
            var teachingFunctions = _unitWork.TeachingFunction.GetAll();
            return Json(new { data = teachingFunctions });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteTeachingFunction(int id)
        {
            TeachingFunction teachingFunctionDb = new TeachingFunction();
            // Actualiza el registro
            teachingFunctionDb = _unitWork.TeachingFunction.Get(id);

            if (teachingFunctionDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar función docente! " });
            }

            teachingFunctionDb.Active = false;
            _unitWork.TeachingFunction.Update(teachingFunctionDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Tipo de función docente borrada exitosamente" });

        }


        #endregion
    }
}
