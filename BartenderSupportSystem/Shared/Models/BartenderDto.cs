using System;

namespace BartenderSupportSystem.Shared.Models
{
    public sealed class BartenderDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoPath { get; set; }
    }
}
