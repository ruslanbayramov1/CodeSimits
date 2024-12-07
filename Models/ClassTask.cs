namespace CodeSimits.Models
{
    public class ClassTask : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public string Type { get; set; }

        public double MaxGradePoint { get; set; } 

        public DateTime DeadLine { get; set; }
        public ICollection<Grade> Grades { get; set; }
    }
}
