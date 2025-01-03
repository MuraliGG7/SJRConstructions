﻿using SJRConstructions.Core.Entities;
using SJRConstructions.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SJRConstructions.Infrastructure.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EmailRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<EmailService> SaveInvitationAsync(EmailService invitation)
        {
            await dbContext.AddAsync(invitation);
            await dbContext.SaveChangesAsync();
            return invitation;            
        }
        public EmailService GetEmailByGuId(string guid)
        {
            return dbContext.EmailServices.FirstOrDefault(u => u.InviteToken == guid);
        }
    }
}
