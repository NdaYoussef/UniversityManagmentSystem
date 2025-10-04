using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniManagementSystem.Domain.Models
{
    public class RefreshToken
    {
        public int id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsExpired => DateTime.UtcNow > ExpiresOn;
        public bool IsActive => !IsExpired && RevokedOn == null;
    }
}
