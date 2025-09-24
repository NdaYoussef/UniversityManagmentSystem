namespace UniManagementSystem.Domain.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxDegree { get; set; }
        public int MinDegree { get; set; }
        public bool IsActive { get; set; }  
        public string CourseCode { get; set; }


        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    }
}
