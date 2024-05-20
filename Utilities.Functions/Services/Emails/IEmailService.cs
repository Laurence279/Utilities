using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Functions.Services.Emails
{
    public interface IEmailService
    {
        Task SendAsync(Email email);
    }
}
