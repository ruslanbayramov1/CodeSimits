namespace CodeSimits.ViewModels
{
    public class StudentMaterial
    {
        public int CourseId { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }
    }
}
