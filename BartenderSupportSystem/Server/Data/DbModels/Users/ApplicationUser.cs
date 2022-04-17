using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace BartenderSupportSystem.Server.Data.DbModels.Users
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        [Range(0, int.MaxValue)] public int CustomerId { get; set; }
    }
}