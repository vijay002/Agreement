using Agreement.Domain.Account;
using Agreement.Domain.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Agreement.Services.Account
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserDetailByUsername(string username);

        Task<bool> IsAccountActivatedAsync(string email, UserManager<ApplicationUser> userManager);
    }
}
