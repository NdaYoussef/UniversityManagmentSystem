using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniManagementSystem.Domain.Models
{
    public class StudentExam
    {
        [ForeignKey(nameof(StudentId))]
        public ApplicationUser Student { get; set; }
        [Required]
        public string StudentId { get; set; }

        [Required]
        public int ExamId { get; set; }
        [ForeignKey(nameof(ExamId))]
        public Exam Exam { get; set; }

        public Decimal? Grade { get; set; }

    }
}
