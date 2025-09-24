namespace UniManagementSystem.Domain.Models
{
    public class Fee
    {
        public int Id { get; set; } 
        public decimal Amount {  get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
        public ApplicationUser Student { get; set; }
        public string StudentID { get; set; }
    }
}
