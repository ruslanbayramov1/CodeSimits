namespace CodeSimits.Models
{
    public class Material : BaseEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }

        public DateTime UploadedAt { get; set; }
        public ICollection<ClassTask> ClassTasks { get; set; }
    }
}
