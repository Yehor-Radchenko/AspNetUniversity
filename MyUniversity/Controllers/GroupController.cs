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
        public async Task<IActionResult> Index()
        {
            return View(await _groupService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.AllCourses = await _groupService.GetCourseSelectList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Group group)
        {
           await _groupService.Create(group);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is not null)
            {
                await _groupService.Delete(id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {

                Group? group = await _groupService.GetById(id);
                
                if (group is not null)
                {
                    ViewBag.AllCourses = await _groupService.GetCourseSelectList(group);
                    return View(group);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Group group)
        {
            await _groupService.Update(group);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Group? group = await _groupService.GetById(id);
                List<Student> students = await _groupService.GetStudents(id);
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
