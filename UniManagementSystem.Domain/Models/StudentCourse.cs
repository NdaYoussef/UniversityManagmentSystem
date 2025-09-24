namespace UniManagementSystem.Domain.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public ApplicationUser Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
