using MyUniversity.Models;

namespace MyUniversity.Services.Interfaces
{
    public interface ICourseService
    {
        public Task<IEnumerable<Course>> GetAll();
        public Task Create(Course model);
        public Task<Course?> GetById(int? id);
        public Task Update(Course expectedEntityValues);
        public Task Delete(int? id);
    }
}
