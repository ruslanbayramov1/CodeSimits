using CodeSimits.Contexts;
using CodeSimits.CustomExtention;
using CodeSimits.Models;
using CodeSimits.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;


namespace CodeSimits.Areas.Admin.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;

        }
        

        public IActionResult Index()
        {
            var student = _context.Users.FirstOrDefault(n=>n.Id == ClaimTypes.NameIdentifier);
            var tasks = _context.Tasks.FirstOrDefault();


            return View(student);
        }

        public IActionResult Update()
        {
           
            AppUser user = _context.Users.Find(ClaimTypes.NameIdentifier);
           
            return View(user);
        }

        [HttpPost]
        public IActionResult Update(AppUser user) 
        {


            _context.Users.Update(user);


            return RedirectToAction("Index");
        }

       
        public async Task<IActionResult> GetGroup()
        {

            var course = await _context.Courses
                .Where(u => u.UsersId == ClaimTypes.NameIdentifier)
                .Select(u => new GetCourse
                {
                    Name = u.Name,
                    Description = u.Description,
                    Id = u.Id,
                }).ToListAsync();

            ViewBag.Grades = _context.Grades.Where(n => n.StudentId == ClaimTypes.NameIdentifier).ToList();


            return View(course);
        }

        public async Task<IActionResult> GetGroupDetail(int id)
        {

            var groupDetail = _context.Courses.FirstOrDefault(n => n.Id == id);

            GetCourseDetail courseDetailVm = new GetCourseDetail
            {
                Name = groupDetail.Name,
                Description = groupDetail.Description,
                Schedule = groupDetail.Schedule,
                GoneLimit = groupDetail.GoneLimit,
            };

            return View(courseDetailVm);
        }

        public async Task<IActionResult> Grades(int id)
        {

            var Grades = _context.Grades.Where(n=>n.Id== id).FirstOrDefault();

            ViewBag.Task = _context.Tasks.Where(n => n.Id == Grades.ClassTaskId).FirstOrDefault();

            return View(Grades);
        }

        public  async Task<IActionResult> AddTaskDocumant( StudentMaterial materialVM,IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(materialVM);
            }
            var uploadedFileName = await file.FileUpload("wwwroot/Files", 15);

            Material material = new Material
            {
                FileName = uploadedFileName,
                CourseId = materialVM.CourseId,
            };


            return RedirectToAction(nameof(Grades));
        }





    }
}
