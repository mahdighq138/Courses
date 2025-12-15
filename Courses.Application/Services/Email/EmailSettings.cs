using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Application.Services.Email
{
    public class EmailSettings
    {
        public required string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string FromEmail { get; set; }
        public required string FromName { get; set; }
    }
}
