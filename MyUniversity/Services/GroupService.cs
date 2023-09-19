using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyUniversity.Data;
using MyUniversity.Models;
using MyUniversity.Services.Interfaces;

namespace MyUniversity.Services
{
    public class GroupService : IGroupService
    {
        private readonly MyUniversityDbContext _context;
        public GroupService(MyUniversityDbContext context)
        {
            _context = context;
        }
        public async Task Create(Group model)
        {
            if (_context.Groups.FirstOrDefault(c => c.Name == model.Name) is not null)
            {
                throw new Exception("Group with this name is already exists.");
            }
            _context.Groups.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            Group group = new Group { Id = id.Value };
            _context.Entry(group).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            return await _context.Groups.Include(g => g.Course).ToListAsync();
        }

        public async Task<Group?> GetById(int? id)
        {
            Group? group = await _context.Groups.Include(g => g.Students).FirstOrDefaultAsync(p => p.Id == id);
            return group;
        }

        public async Task Update(Group expectedEntityValues)
        {
            var existingGroup = await _context.Groups.FindAsync(expectedEntityValues.Id);
            if (existingGroup is null)
            {
                throw new ArgumentException("Group with the specified ID does not exist.");
            }
            if (await _context.Groups.FirstOrDefaultAsync(c => c.Name == expectedEntityValues.Name) is not null)
            {
                throw new Exception("Group with this name is already exists.");
            }
            _context.Groups.Update(expectedEntityValues);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SelectListItem>> GetCourseSelectList(Group? group)
        {
            var allCourses = await _context.Courses.ToListAsync();

            // Create a SelectListItem list for the dropdown
            return allCourses.Select(course => new SelectListItem
            {
                Text = course.Name,
                Value = course.Id.ToString(),
                Selected = course.Id == group.CourseId
            }).ToList();
        }

        public async Task<List<Student>> GetStudents(Group? group)
        {
            return await _context.Students.Where(s => s.GroupId == group.Id).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetCourseSelectList()
        {
            var allCourses = await _context.Courses.ToListAsync();

            return allCourses.Select(course => new SelectListItem
            {
                Text = course.Name,
                Value = course.Id.ToString(),
            }).ToList();
        }
    }
}
