using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Pedigree.Infra.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
        }

        public ApplicationUser(string userName, string email)
        {
            Email = email;
            UserName = userName;
        }
    }
}