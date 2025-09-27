using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniManagementSystem.Domain.Models
{
    public class Fee
    {
        public int Id { get; set; }
        [Required]
        public decimal Amount {  get; set; }
        public DateTime Date { get; set; }
        public bool IsPaid { get; set; }
        [ForeignKey(nameof(StudentId))]
        public ApplicationUser Student { get; set; }
        [Required]
        public string StudentId { get; set; }
    }
}
