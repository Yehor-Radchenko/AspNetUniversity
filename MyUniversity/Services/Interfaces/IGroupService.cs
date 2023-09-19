using Microsoft.AspNetCore.Mvc.Rendering;
using MyUniversity.Models;

namespace MyUniversity.Services.Interfaces
{
    public interface IGroupService
    {
        public Task<IEnumerable<Group>> GetAll();
        public Task Create(Group model);
        public Task<Group?> GetById(int? id);
        public Task Update(Group expectedEntityValues);
        public Task Delete(int? id);
        public Task<List<SelectListItem>> GetCourseSelectList(Group? group);
        public Task<List<SelectListItem>> GetCourseSelectList();
        public Task<List<Student>> GetStudents(Group? group);
    }
}
