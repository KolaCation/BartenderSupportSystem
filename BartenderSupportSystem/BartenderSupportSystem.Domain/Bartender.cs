using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain
{
    public sealed class Bartender
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public double Experience { get; }
        public string PhotoPath { get; }

        public Bartender(Guid id, string firstName, string lastName, double experience, string photoPath)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Experience = experience;
            PhotoPath = photoPath;
        }
    }
}
