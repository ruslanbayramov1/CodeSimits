namespace CodeSimits.Models
{
    public class Grade : BaseEntity
    {
        public string StudentId { get; set; }
        public AppUser Student { get; set; }

        public double GradePoint { get; set; }

        public DateTime EvalutedAt { get; set; }

        public int ClassTaskId { get; set; }
        public ClassTask ClassTask { get; set; }
    }
}
