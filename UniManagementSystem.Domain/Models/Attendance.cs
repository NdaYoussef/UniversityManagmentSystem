using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniManagementSystem.Domain.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool IsPresent { get; set; } = false;
        [ForeignKey(nameof(StudentId))]
        public ApplicationUser Student { get; set; }
        [Required]
        public string StudentId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
        [Required]
        public int CourseId { get; set; }

    }
}
