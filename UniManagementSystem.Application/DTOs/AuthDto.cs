using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniManagementSystem.Application.DTOs
{
    internal class AuthDto
    {
        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; } = false;
        public string? Token { get; set; }
        public object? Data { get; set; }
    }
}
