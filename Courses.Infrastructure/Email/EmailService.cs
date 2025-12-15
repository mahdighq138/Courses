using Courses.Application.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
