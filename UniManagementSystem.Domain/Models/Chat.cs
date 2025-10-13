using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniManagementSystem.Domain.Models
{
    public class Chat
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public ApplicationUser User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
