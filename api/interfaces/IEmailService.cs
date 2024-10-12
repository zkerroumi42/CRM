using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.interfaces
{
    public interface IEmailService
    {
        Task SendPasswordResetEmail(string toEmail, string resetToken);
    }
}