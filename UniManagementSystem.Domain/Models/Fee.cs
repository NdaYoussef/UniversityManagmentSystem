using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniManagementSystem.Domain.Models
{
    public class Fee
    {
        public int Id { get; set; }
        [Required]
        public decimal Amount {  get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool IsPaid { get; set; } = false;
        public ApplicationUser Student { get; set; }
        [Required]
        [ForeignKey("Student")]
        public string StudentId { get; set; }
    }
}
