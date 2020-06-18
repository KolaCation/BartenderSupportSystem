using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTimeOffset RegistrationDate { get; set; }
        public Guid BartenderId { get; set; }
    }
}
