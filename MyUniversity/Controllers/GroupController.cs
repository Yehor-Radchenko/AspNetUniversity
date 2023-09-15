using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyUniversity.Data;
using MyUniversity.Models;
using MyUniversity.Services;
using MyUniversity.Services.Interfaces;

namespace MyUniversity.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService service)
        {
            _groupService = service;
        }
        public IActionResult Index()
        {
            return View(_groupService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllCourses = _groupService.GetCourseSelectList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Group group)
        {
           _groupService.Create(group);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is not null)
            {
                _groupService.Delete(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {

                Group? group = _groupService.GetById(id);
                
                if (group is not null)
                {
                    ViewBag.AllCourses = _groupService.GetCourseSelectList(group);
                    return View(group);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Group group)
        {
            _groupService.Update(group);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id != null)
            {
                Group? group = _groupService.GetById(id);
                List<Student> students = _groupService.GetStudents(group);
                if (group != null)
                {
                    group.Students = students;
                    return View(group);
                }
            }
            return NotFound();
        }
    }
}
