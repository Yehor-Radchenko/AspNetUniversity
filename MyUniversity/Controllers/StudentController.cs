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

        public async Task<IActionResult> Index()
        {
            return View(await _studentService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.AllGroups = await _studentService.GetGroupSelectList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            await _studentService.Create(student);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is not null)
            {
                await _studentService.Delete(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Student? student = await _studentService.GetById(id);

                if (student is not null)
                {
                    ViewBag.AllGroups = await _studentService.GetGroupSelectList(student);
                    return View(student);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            await _studentService.Update(student);
            return RedirectToAction("Index");
        }

    }
}
