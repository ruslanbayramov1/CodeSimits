namespace CodeSimits.Models
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }

        public string UsersId { get; set; }
        public AppUser Users { get; set; }

        public string Description { get; set; }

        public DateTime Schedule { get; set; }

        public int GoneLimit { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Material> Materials { get; set; }
    
    }
}
