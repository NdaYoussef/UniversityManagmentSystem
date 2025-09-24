namespace UniManagementSystem.Domain.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}
