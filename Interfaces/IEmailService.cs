using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationBackendFinal.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(List<string> email, string subject, string message);
    }
}
