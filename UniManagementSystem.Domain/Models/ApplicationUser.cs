using Microsoft.AspNetCore.Identity;
using UniManagementSystem.Domain.Enums;

namespace UniManagementSystem.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public string ProfilePic {  get; set; }
        public string NationalID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double GPA { get; set; }
        public DateTime EnrolmentDate { get; set; }
        public Roles Role {  get; set; }
        public decimal? Salary { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<StudentExam>? StudentExams { get; set; } = new List<StudentExam>();
        public ICollection<Attendance>? Attendances { get; set; } = new List<Attendance>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();


    }
}
