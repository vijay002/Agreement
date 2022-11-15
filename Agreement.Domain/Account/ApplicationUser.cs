using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agreement.Domain.Account
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        
        }

        [PersonalData]
        public string CreatedBy { get; set; }
        [PersonalData]
        public string UpdatedBy { get; set; }
        [PersonalData]
        public DateTime CreatedDate { get; set; }
        [PersonalData]
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
    }
}
