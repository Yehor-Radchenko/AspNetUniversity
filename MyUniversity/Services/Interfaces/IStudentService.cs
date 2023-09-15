using Microsoft.AspNetCore.Mvc.Rendering;
using MyUniversity.Models;

namespace MyUniversity.Services.Interfaces
{
    public interface IStudentService
    {
        public IEnumerable<Student> GetAll();
        public void Create(Student model);
        public Student? GetById(int? id);
        public void Update(Student expectedEntityValues);
        public void Delete(int? id);
        public List<SelectListItem> GetGroupSelectList(Student? student);
        public List<SelectListItem> GetGroupSelectList();
    }
}
