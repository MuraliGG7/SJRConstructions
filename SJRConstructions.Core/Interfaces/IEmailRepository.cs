using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SJRConstructions.Core.Entities;

namespace SJRConstructions.Core.Interfaces
{
    public interface IEmailRepository
    {
        Task<EmailService> SaveInvitationAsync(EmailService invitation);
        EmailService GetEmailByGuId(string guid);
    }
}