using Microsoft.EntityFrameworkCore;
using MyUniversity.Data;
using MyUniversity.Models;
using MyUniversity.Services.Interfaces;
using System.Data;

namespace MyUniversity.Services
{
    public class CourseService : ICourseService
    {
        private readonly MyUniversityDbContext _context;
        public CourseService(MyUniversityDbContext context)
        {
            _context = context;
        }
        public async Task Create(Course model)
        {
            if (_context.Courses.FirstOrDefault(c => c.Name == model.Name) is not null)
            {
                throw new Exception("Course with this name is already exists.");
            }
            _context.Courses.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {            
            Course course = new Course { Id = id.Value };
            _context.Entry(course).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<Course?> GetById(int? id)
        {
            Course? course = await _context.Courses.FirstOrDefaultAsync(p => p.Id == id);
            List<Group> groups = await _context.Groups.Where(g => g.Course.Id == id).ToListAsync();
            course.Groups = groups;
            return course;
        }

        public async Task Update(Course expectedEntityValues)
        {
            _context.Courses.Update(expectedEntityValues);
            await _context.SaveChangesAsync();
        }
    }
}
