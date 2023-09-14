using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyUniversity.Data;
using MyUniversity.Models;

namespace MyUniversity.Controllers
{
    public class StudentController : Controller
    {
        private readonly MyUniversityDbContext _context;

        public StudentController(MyUniversityDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Students
            .Include(s => s.Group)
            .ToList());
        }

        public async Task<IActionResult> Create(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is not null)
            {
                Student student = new Student { Id = id.Value };
                _context.Entry(student).State = EntityState.Deleted;
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

                Student? student = await _context.Students.FirstOrDefaultAsync(p => p.Id == id);

                if (student is not null)
                {
                    var allGroups = _context.Groups.ToList();

                    var groupSelectList = allGroups.Select(group => new SelectListItem
                    {
                        Text = group.Name,
                        Value = group.Id.ToString(),
                        Selected = group.Id == student.Group.Id
                    }).ToList();

                    ViewBag.AllGroups = groupSelectList;

                    return View(student);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
