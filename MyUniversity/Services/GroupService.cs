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
        public void Create(Group model)
        {
            if (_context.Groups.FirstOrDefault(c => c.Name == model.Name) is not null)
            {
                throw new Exception("Group with this name is already exists.");
            }
            _context.Groups.Add(model);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            Group group = new Group { Id = id.Value };
            _context.Entry(group).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public IEnumerable<Group> GetAll()
        {
            return _context.Groups.Include(g => g.Course).ToList();
        }

        public Group? GetById(int? id)
        {
            Group? group = _context.Groups.FirstOrDefault(p => p.Id == id);
            List<Student> students = _context.Students.Where(g => g.Group.Id == id).ToList();
            group.Students = students;
            return group;
        }

        public void Update(Group expectedEntityValues)
        {
            _context.Groups.Update(expectedEntityValues);
            _context.SaveChanges();
        }

        public List<SelectListItem> GetCourseSelectList(Group? group)
        {
            var allCourses = _context.Courses.ToList();

            // Create a SelectListItem list for the dropdown
            return allCourses.Select(course => new SelectListItem
            {
                Text = course.Name,
                Value = course.Id.ToString(),
                Selected = course.Id == group.CourseId
            }).ToList();
        }

        public List<Student> GetStudents(Group? group)
        {
            return _context.Students.Where(s => s.GroupId == group.Id).ToList();
        }

        public List<SelectListItem> GetCourseSelectList()
        {
            var allCourses = _context.Courses.ToList();

            return allCourses.Select(course => new SelectListItem
            {
                Text = course.Name,
                Value = course.Id.ToString(),
            }).ToList();
        }
    }
}
