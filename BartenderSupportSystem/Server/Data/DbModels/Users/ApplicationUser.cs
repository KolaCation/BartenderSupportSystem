using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BartenderSupportSystem.Server.Data.DbModels.Users
{
    public class ApplicationUser : IdentityUser
    {
        public DateTimeOffset RegistrationDate { get; set; }
        [Range(0, int.MaxValue)] public int CustomerId { get; set; }
    }
}