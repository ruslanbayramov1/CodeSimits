namespace CodeSimits.Models
{
    public class Enrollment : BaseEntity
    {
         public string StudentId { get; set; }
         public int CourseId { get; set; }

         public AppUser Student { get; set; }
         public Course Course { get; set; }

         public DateTime EnrolledAt { get; set; }

    }
}
