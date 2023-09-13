using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyUniversity.Data;
using MyUniversity.Models;

namespace MyUniversity.Controllers
{
    public class CourseController : Controller
    {
        MyUniversityDbContext _context;
        public CourseController(MyUniversityDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Courses.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is not null)
            {
                Course course = new Course { Id = id.Value };
                _context.Entry(course).State = EntityState.Deleted;
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
                Course? course = await _context.Courses.FirstOrDefaultAsync(p => p.Id == id);
                if (course != null) 
                    return View(course);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Course? course = await _context.Courses.FirstOrDefaultAsync(p => p.Id == id);
                List<Group> groups = _context.Groups.Where(g => g.Course.Id == id).ToList();
                if (course != null)
                {
                    course.Groups = groups;
                    return View(course);
                }
            }
            return NotFound();
        }
    }
}
