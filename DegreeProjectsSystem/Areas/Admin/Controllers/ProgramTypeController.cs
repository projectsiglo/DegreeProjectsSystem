using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using DegreeProjectsSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProgramTypeController : Controller
    {
        private readonly IUnitWork _unitWork;

        public ProgramTypeController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateProgramType(int? id)
        {
            ProgramTypeViewModel programTypeViewModel = new ProgramTypeViewModel()
            {
                ProgramType = new ProgramType(),
                EducationLevelList = _unitWork.EducationLevel.GetAll().Select(el => new SelectListItem
                {
                    Text = el.Name,
                    Value = el.Id.ToString()
                })
            };

            if (id == null)
            {
                programTypeViewModel.ProgramType.Active = true;
                // Crea un nuevo registro
                return View(programTypeViewModel);
            }

            // Actualiza el registro
            programTypeViewModel.ProgramType = _unitWork.ProgramType.Get(id.GetValueOrDefault());
            if (programTypeViewModel == null)
            {
                return NotFound();
            }
            return View(programTypeViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateProgramType(ProgramType programType)
        {
            if (ModelState.IsValid)
            {
                if (programType.Id == 0)
                {
                    _unitWork.ProgramType.Add(programType);
                }
                else
                {
                    _unitWork.ProgramType.Update(programType);
                }
                _unitWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(programType);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllProgramTypes()
        {
            var programTypes = _unitWork.ProgramType.GetAll(includeProperties: "EducationLevel");
            return Json(new { data = programTypes });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteProgramType(int id)
        {
            ProgramType programTypeDb = new ProgramType();
            // Actualiza el registro
            programTypeDb = _unitWork.ProgramType.Get(id);

            if (programTypeDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar tipo de programa!! " });
            }

            programTypeDb.Active = false;
            _unitWork.ProgramType.Update(programTypeDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Tipo de programa borrado exitosamente" });

        }


        #endregion
    }
}
