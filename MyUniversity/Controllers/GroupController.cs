using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyUniversity.Data;
using MyUniversity.Models;

namespace MyUniversity.Controllers
{
    public class GroupController : Controller
    {
        MyUniversityDbContext _context;
        public GroupController(MyUniversityDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Groups
            .Include(g => g.Course)
            .ToList());
        }

        public async Task<IActionResult> Create(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is not null)
            {
                Group group = new Group { Id = id.Value };
                _context.Entry(group).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {

                Group? group = await _context.Groups.FirstOrDefaultAsync(p => p.Id == id);
                
                if (group is not null)
                {
                    var allCourses = _context.Courses.ToList();

                    // Create a SelectListItem list for the dropdown
                    var courseSelectList = allCourses.Select(course => new SelectListItem
                    {
                        Text = course.Name,
                        Value = course.Id.ToString(),
                        Selected = course.Id == group.Course.Id
                    }).ToList();

                    ViewBag.AllCourses = courseSelectList;

                    return View(group);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Group group)
        {
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Group? group = await _context.Groups.FirstOrDefaultAsync(p => p.Id == id);
                List<Student> students = _context.Students.Where(g => g.Group.Id == id).ToList();
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
