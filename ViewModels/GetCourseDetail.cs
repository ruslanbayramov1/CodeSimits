namespace CodeSimits.ViewModels
{
    public class GetCourseDetail
    {
        public int TeacherId { get; set; }
        public string Name { get;set; }
        public string Description { get;set; }
        public DateTime Schedule { get; set; }

        public int GoneLimit { get; set; }

    }
}
