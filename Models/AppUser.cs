using Microsoft.AspNetCore.Identity;

namespace CodeSimits.Models
{
    public class AppUser : IdentityUser
    {

        public string Id {  get; set; }
        public string? ProfilePhoto { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Grade> Grades { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
