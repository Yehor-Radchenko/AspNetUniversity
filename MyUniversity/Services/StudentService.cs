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
        public async Task Create(Student model)
        {
            if(model.FirstName.IsNullOrEmpty() || model.LastName.IsNullOrEmpty())
            {
                throw new Exception("Students name and surname can't be empty.");
            }
            _context.Students.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            Student student = new Student { Id = id.Value };
            _context.Entry(student).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await  _context.Students.Include(s => s.Group).ToListAsync();
        }

        public async Task<Student?> GetById(int? id)    
        {
            return await _context.Students.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<SelectListItem>> GetGroupSelectList(Student? student)
        {
            var allGroups = await _context.Groups.ToListAsync();

            // Create a SelectListItem list for the dropdown
            return allGroups.Select(group => new SelectListItem
            {
                Text = group.Name,
                Value = group.Id.ToString(),
                Selected = student.GroupId == group.Id
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetGroupSelectList()
        {
            var allGroups = await  _context.Groups.ToListAsync();

            return allGroups.Select(group => new SelectListItem
            {
                Text = group.Name,
                Value = group.Id.ToString(),
            }).ToList();
        }

        public async Task Update(Student expectedEntityValues)
        {
            _context.Students.Update(expectedEntityValues);
            await _context.SaveChangesAsync();
        }
    }
}
