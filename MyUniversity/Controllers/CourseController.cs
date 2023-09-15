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
        public IActionResult Index()
        {
            return View(_courseService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            _courseService.Create(course);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is not null)
            {
                _courseService.Delete(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is not null)
            {
                Course? course = _courseService.GetById(id);
                if (course is not null) 
                    return View(course);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            _courseService.Update(course);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is not null)
            {
                Course course = _courseService.GetById(id);
                return View(course);
            }
            return NotFound();
        }
    }
}
