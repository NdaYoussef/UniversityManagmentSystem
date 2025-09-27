using System.ComponentModel.DataAnnotations;

namespace UniManagementSystem.Domain.Models
{
    public class Chat
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        //public DateTime SentAt { get; set; }
    }
}
