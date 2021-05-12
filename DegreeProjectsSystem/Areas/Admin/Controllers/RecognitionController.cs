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
    public class RecognitionController : Controller
    {
        private readonly IUnitWork _unitWork;
        private readonly INotyfService _notyfService;
        public RecognitionController(IUnitWork unitWork, INotyfService notyfService)
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

        public IActionResult InsertOrUpdateRecognition(int? id)
        {
            RecognitionViewModel recognitionViewModel = new RecognitionViewModel()
            {
                Recognition = new Recognition(),
                EducationLevelList = _unitWork.EducationLevel.GetAll(orderBy: el => el.OrderBy(el => el.Name)).Select(el => new SelectListItem
                {
                    Text = el.Name,
                    Value = el.Id.ToString()
                })
            };

            if (id == null)
            {
                recognitionViewModel.Recognition.Active = true;
                // Crea un nuevo registro
                return View(recognitionViewModel);
            }

            // Actualiza el registro
            recognitionViewModel.Recognition = _unitWork.Recognition.Get(id.GetValueOrDefault());
            if (recognitionViewModel == null)
            {
                return NotFound();
            }
            return View(recognitionViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOrUpdateRecognition(RecognitionViewModel recognitionViewModel)
        {
            Action action = Action.None;
            if (ModelState.IsValid)
            {
                if (recognitionViewModel.Recognition.Id == 0)
                {
                    action = Action.Create;
                    _unitWork.Recognition.Add(recognitionViewModel.Recognition);
                }
                else
                {
                    action = Action.Update;
                    _unitWork.Recognition.Update(recognitionViewModel.Recognition);
                }

                try
                {
                    _unitWork.Save();

                    if (action == Action.Create)
                    {
                        _notyfService.Success("Reconocimiento creado correctamente.");
                    }
                    if (action == Action.Update)
                    {
                        _notyfService.Success("Reconocimiento actualizado correctamente.");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("IX_Recognitions_Name"))
                    {
                        _notyfService.Error("Ya existe un Reconocimiento con la misma descripción.");

                        recognitionViewModel.EducationLevelList = _unitWork.EducationLevel.GetAll(orderBy: el => el.OrderBy(el => el.Name)).Select(el => new SelectListItem
                        {
                            Text = el.Name,
                            Value = el.Id.ToString()
                        });

                        return View(recognitionViewModel);
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
                recognitionViewModel.EducationLevelList = _unitWork.EducationLevel.GetAll(orderBy: el => el.OrderBy(el => el.Name)).Select(el => new SelectListItem
                {
                    Text = el.Name,
                    Value = el.Id.ToString()
                });

                if (recognitionViewModel.Recognition.Id != 0)
                {
                    recognitionViewModel.Recognition = _unitWork.Recognition.Get(recognitionViewModel.Recognition.Id);
                }
            }
            return View(recognitionViewModel);
        }

        #region API
        [HttpGet]
        public IActionResult GetAllRecognitions()
        {
            var recognitions = _unitWork.Recognition.GetAll(includeProperties: "EducationLevel");
            return Json(new { data = recognitions });
        }

        //Eliminación lógica de registro 
        [HttpPost]
        public IActionResult DeleteRecognition(int id)
        {
            // Actualiza el registro
            var recognitionDb = _unitWork.Recognition.Get(id);

            if (recognitionDb == null)
            {
                return Json(new { succes = false, message = "!!Error al borrar el reconocimiento!!." });
            }

            recognitionDb.Active = false;
            _unitWork.Recognition.Update(recognitionDb);
            _unitWork.Save();


            return Json(new { succes = true, message = "Reconocimiento borrado exitosamente." });

        }


        #endregion
    }
}
