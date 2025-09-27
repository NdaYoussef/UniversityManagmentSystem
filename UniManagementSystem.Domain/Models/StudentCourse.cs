using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniManagementSystem.Domain.Models
{
    public class StudentCourse
    {
        [Required]
        public string StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public ApplicationUser Student { get; set; }

        [Required]
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
    }
}
