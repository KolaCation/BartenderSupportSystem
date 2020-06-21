using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain
{
    public sealed class Bartender
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public double Experience { get; private set; }
        public string PhotoPath { get; private set; }

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
