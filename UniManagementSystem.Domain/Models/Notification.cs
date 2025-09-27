using System.ComponentModel.DataAnnotations;
using UniManagementSystem.Domain.Enums;

namespace UniManagementSystem.Domain.Models
{
    public class Notification
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public Roles RecepetiveRole { get; set; }
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }

    }
}
