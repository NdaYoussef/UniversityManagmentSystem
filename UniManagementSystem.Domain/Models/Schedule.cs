namespace UniManagementSystem.Domain.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public ApplicationUser Instructor { get; set; }
        public string InstructorId { get; set; }

    }
}
