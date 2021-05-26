using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radio_Player.Authentication
{
    public class ApplicationUser : IdentityUser
    {   
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
