using Microsoft.AspNetCore.Mvc.Rendering;
using MyUniversity.Models;

namespace MyUniversity.Services.Interfaces
{
    public interface IGroupService
    {
        public IEnumerable<Group> GetAll();
        public void Create(Group model);
        public Group? GetById(int? id);
        public void Update(Group expectedEntityValues);
        public void Delete(int? id);
        public List<SelectListItem> GetCourseSelectList(Group? group);
        public List<SelectListItem> GetCourseSelectList();
        public List<Student> GetStudents(Group? group);
    }
}
