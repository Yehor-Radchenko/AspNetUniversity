using Microsoft.AspNetCore.Mvc.Rendering;
using MyUniversity.Models;

namespace MyUniversity.Services.Interfaces
{
    public interface IStudentService
    {
        public Task Create(Student model);
        public Task Delete(int? id);
        public Task<IEnumerable<Student>> GetAll();
        public Task<Student?> GetById(int? id);
        public Task<List<SelectListItem>> GetGroupSelectList(Student? student);
        public Task<List<SelectListItem>> GetGroupSelectList();
        public Task Update(Student expectedEntityValues);
    }
}
