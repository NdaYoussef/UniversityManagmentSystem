using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UniManagementSystem.Domain.Enums;

namespace UniManagementSystem.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<RefreshToken> refreshTokens = new List<RefreshToken>();

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        public string Gender { get; set; }

        public string ProfilePic { get; set; }
        [MaxLength(20)]
        public string NationalID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double GPA { get; set; }
        public DateTime EnrolmentDate { get; set; }
        public Roles Role { get; set; }
        public decimal? Salary { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<StudentExam>? StudentExams { get; set; } = new List<StudentExam>();
        public ICollection<Attendance>? Attendances { get; set; } = new List<Attendance>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
       // [JsonIgnore]
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}
