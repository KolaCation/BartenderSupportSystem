using Microsoft.AspNetCore.Identity;
using System;

namespace BartenderSupportSystem.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTimeOffset RegistrationDate { get; set; }
        public int BartenderId { get; set; }
    }
}
