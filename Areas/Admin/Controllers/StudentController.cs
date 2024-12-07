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
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;

        }
        

        public async Task<IActionResult> IndexAsync()
        {
            var student = _context.Users.FirstOrDefault(n=>n.Id == ClaimTypes.NameIdentifier);

          

            //StudentIndex studentIndex = new StudentIndex 
            //{
            //    Username = student.UserName,
            //    Name = student.Name,
            //    Surname = student.Surname,
            //    Email = student.Email,
               
            //};

            var grades = _context.Grades.Where(n=>n.StudentId == ClaimTypes.NameIdentifier).ToList();

            double a = 0;

            foreach (var item in grades)
            {
                a += item.GradePoint;
            }

            var overAll = a/grades.Count;


            var course = await _context.Courses
                .Where(u => u.UsersId == ClaimTypes.NameIdentifier)
                .Select(u => new GetCourse
                {
                    Name = u.Name,
                    Description = u.Description,
                    Id = u.Id,
                }).ToListAsync();


            ViewBag.Grades = _context.Grades.Where(n => n.StudentId == ClaimTypes.NameIdentifier).ToList();

            ViewBag.OverAll = overAll;
            ViewBag.TotalGrades = grades.Count;
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

       
       /* public async Task<IActionResult> GetGroup()
        {

            


            return View(course);
        }*/

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
