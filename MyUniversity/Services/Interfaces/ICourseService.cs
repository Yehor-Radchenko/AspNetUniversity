using MyUniversity.Models;

namespace MyUniversity.Services.Interfaces
{
    public interface ICourseService
    {
        public IEnumerable<Course> GetAll();
        public void Create(Course model);
        public Course? GetById(int? id);
        public void Update(Course expectedEntityValues);
        public void Delete(int? id);
    }
}
