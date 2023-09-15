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
        public void Create(Course model)
        {
            if (_context.Courses.FirstOrDefault(c => c.Name == model.Name) is not null)
            {
                throw new Exception("Course with this name is already exists.");
            }
            _context.Courses.Add(model);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {            
            Course course = new Course { Id = id.Value };
            _context.Entry(course).State = EntityState.Deleted;
            _context.SaveChanges();
        }
        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public Course? GetById(int? id)
        {
            Course? course = _context.Courses.FirstOrDefault(p => p.Id == id);
            List<Group> groups = _context.Groups.Where(g => g.Course.Id == id).ToList();
            course.Groups = groups;
            return course;
        }

        public void Update(Course expectedEntityValues)
        {
            _context.Courses.Update(expectedEntityValues);
            _context.SaveChanges();
        }
    }
}
