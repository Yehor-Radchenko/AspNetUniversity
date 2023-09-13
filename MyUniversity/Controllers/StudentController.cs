using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyUniversity.Data;

namespace MyUniversity.Controllers
{
    public class StudentController : Controller
    {
        private readonly MyUniversityDbContext _context;

        public StudentController(MyUniversityDbContext context)
        {
            _context = context;
        }
    }
}
