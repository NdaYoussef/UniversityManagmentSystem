namespace UniManagementSystem.Domain.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; } = false;
        public ApplicationUser Student { get; set; }
        public string StudentID { get; set; }
        public Course Course { get; set; }
        public int CourseID { get; set; }

    }
}
