using CodeSimits.Areas.Admin.ViewModel.Teacher;
using CodeSimits.Contexts;
using CodeSimits.Extensions;
using CodeSimits.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CodeSimits.Areas.Admin.Controllers
{
    public class TeacherController(AppDbContext _context) : Controller
    {
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


            return View(course);
        }

        public async Task<IActionResult> GetStudent(int courseId)
        {

            var student = await _context.Enrollments
                .Include(u => u.Student)
                .Where(u => u.CourseId == courseId)
                .Select(u => new GetStudent
                {
                    Id = u.StudentId,
                    Name = u.Student.Name,
                    Surname = u.Student.Surname,
                }).ToListAsync();

            return View(student);
        }

        public async Task<IActionResult> GetTask(int courseId)
        {

            var task = await _context.Tasks
                .Include(u => u.Materials)
                .Where(u => u.Materials.CourseId == courseId)
                .Select(u => new GetTask
                {
                    Id = u.Id,
                    Title = u.Title,
                    Deadline = u.DeadLine
                }).ToListAsync();

            return View(task);
        }

        [HttpGet]
        public async Task<IActionResult> Valued(int courseId)
        {
            var student = await _context.Enrollments
                .Include(u => u.Student)
                .Where(u => u.CourseId == courseId)
                .Select(u => new GetStudent
                {
                    Id = u.StudentId,
                    Name = u.Student.Name,
                    Surname = u.Student.Surname,
                }).ToListAsync();

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Valued(int TaskId, double point, string userId)
        {

            Grade grade = new Grade
            {
                ClassTaskId = TaskId,
                GradePoint = point,
                EvalutedAt = DateTime.Now,
                StudentId = userId
            };

            await _context.Grades.AddAsync(grade);
            await _context.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateMaterial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaterial(int courseId, CeateMaterialVM vm)
        {

            if (vm.FormPaht != null)
            {
                if (!vm.FormPaht.IsValidSize(200))
                {
                    ModelState.AddModelError("FormPaht", "Size Error");
                }
            }

            var photoURL = Path.Combine("imgs", "profilePhoto", vm.FileName);

            Material material = new Material
            {
                FileName = vm.FileName,
                CourseId = courseId,
                UploadedAt = DateTime.UtcNow,
                FilePath = photoURL
            };

            await _context.Materials.AddAsync(material);
            await _context.SaveChangesAsync();

            return View();
        }


    }
}
