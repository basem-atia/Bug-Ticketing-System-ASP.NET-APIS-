using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTicketingSystem.BL
{
    public class TokenDto
    {
        public string token { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
    }
}
