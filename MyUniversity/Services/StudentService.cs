using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyUniversity.Data;
using MyUniversity.Models;
using MyUniversity.Services.Interfaces;

namespace MyUniversity.Services
{
    public class StudentService : IStudentService
    {
        private readonly MyUniversityDbContext _context;
        public StudentService(MyUniversityDbContext context)
        {
            _context = context;
        }
        public void Create(Student model)
        {
            if(model.FirstName.IsNullOrEmpty() || model.LastName.IsNullOrEmpty())
            {
                throw new Exception("Students name and surname can't be empty.");
            }
            _context.Students.Add(model);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            Student student = new Student { Id = id.Value };
            _context.Entry(student).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Students.Include(s => s.Group).ToList();
        }

        public Student? GetById(int? id)
        {
            return _context.Students.FirstOrDefault(p => p.Id == id);
        }

        public List<SelectListItem> GetGroupSelectList(Student? student)
        {
            var allGroups = _context.Groups.ToList();

            // Create a SelectListItem list for the dropdown
            return allGroups.Select(group => new SelectListItem
            {
                Text = group.Name,
                Value = group.Id.ToString(),
                Selected = student.GroupId == group.Id
            }).ToList();
        }

        public List<SelectListItem> GetGroupSelectList()
        {
            var allGroups = _context.Groups.ToList();

            return allGroups.Select(group => new SelectListItem
            {
                Text = group.Name,
                Value = group.Id.ToString(),
            }).ToList();
        }

        public void Update(Student expectedEntityValues)
        {
            _context.Students.Update(expectedEntityValues);
            _context.SaveChanges();
        }
    }
}
