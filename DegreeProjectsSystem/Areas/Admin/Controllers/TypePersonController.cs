using DegreeProjectsSystem.DataAccess.Repository.IRepository;
using DegreeProjectsSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DegreeProjectsSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TypePersonController : Controller
    {
        private readonly IUnitWork _unitWork;

        public TypePersonController(IUnitWork unitWork)
        {
            _unitWork = unitWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InsertOrUpdateTypePerson(int? id)
        {
            TypePerson typePerson = new TypePerson();
            if (id == null)
            {
                // Crea un nuevo registro
                return View(typePerson);
            }
            // Actualiza el registro
            typePerson = _unitWork.TypePerson.Get(id.GetValueOrDefault());
            if (typePerson == null)
            {
                return NotFound();
            }
            return View(typePerson);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateTypePerson(TypePerson typePerson)
        {
            if (ModelState.IsValid)
            {
                if (typePerson.Id == 0)
                {
                    _unitWork.TypePerson.Add(typePerson);
                }
                else
                {
                    _unitWork.TypePerson.Update(typePerson);
                }
                _unitWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(typePerson);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllTypePeople()
        {
            var typePeople = _unitWork.TypePerson.GetAll();
            return Json(new { data = typePeople });
        }

        //Eliminación de registro lógica
        [HttpPost]
        public IActionResult DeleteTypePerson(int id)
        {
            TypePerson typePersonDb = new TypePerson();
            // Actualiza el registro
            typePersonDb = _unitWork.TypePerson.Get(id);

            if (typePersonDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar tipo de persona!! " });
            }

            typePersonDb.Active = false;
            _unitWork.TypePerson.Update(typePersonDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "tipo de persona borrado exitosamente" });

        }


        #endregion
    }
}
