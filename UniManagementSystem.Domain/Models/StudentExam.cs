namespace UniManagementSystem.Domain.Models
{
    public class StudentExam
    {
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public Decimal? Grade { get; set; }

    }
}
