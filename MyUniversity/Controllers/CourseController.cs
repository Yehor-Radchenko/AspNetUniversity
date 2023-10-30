using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyUniversity.Data;
using MyUniversity.Models;
using MyUniversity.Services;
using MyUniversity.Services.Interfaces;

namespace MyUniversity.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService service)
        {
            _courseService = service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _courseService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            await _courseService.Create(course);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is not null)
            {
                await _courseService.Delete(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is not null)
            {
                Course? course = await _courseService.GetById(id);
                if (course is not null) 
                    return View(course);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            await _courseService.Update(course);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is not null)
            {
                Course course = await _courseService.GetById(id);
                return View(course);
            }
            return NotFound();
        }
    }
}
