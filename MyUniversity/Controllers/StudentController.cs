using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyUniversity.Data;
using MyUniversity.Models;
using MyUniversity.Services;
using MyUniversity.Services.Interfaces;

namespace MyUniversity.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService service)
        {
            _studentService = service;
        }

        public IActionResult Index()
        {
            return View(_studentService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllGroups = _studentService.GetGroupSelectList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            _studentService.Create(student);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is not null)
            {
                _studentService.Delete(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {

                Student? student = _studentService.GetById(id);

                if (student is not null)
                {
                    ViewBag.AllGroups = _studentService.GetGroupSelectList(student);
                    return View(student);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            _studentService.Update(student);
            return RedirectToAction("Index");
        }
    }
}
