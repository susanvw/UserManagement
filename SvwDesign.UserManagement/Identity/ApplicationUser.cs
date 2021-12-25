using Microsoft.AspNetCore.Identity;
using System;

namespace SvwDesign.UserManagement
{
    public class ApplicationUser : IdentityUser 
    {
        public DateTime LastUpdateDate { get; set; }
    }
}

