using System.ComponentModel.DataAnnotations;

namespace UniManagementSystem.Domain.Models
{
    public class Exam
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Location { get; set; }
        public ICollection<StudentExam> StudentExams { get; set; } = new List<StudentExam>();
    }
}
