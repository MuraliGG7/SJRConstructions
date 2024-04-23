using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GirlScoutCookieBoothManager.Core.Models;

namespace GirlScoutCookieBoothManager.Infrastructure.Interfaces
{
    public interface IInvitationRepository
    {
        Task<Invitation> SaveInvitationAsync(Invitation invitation);       
    }
}
