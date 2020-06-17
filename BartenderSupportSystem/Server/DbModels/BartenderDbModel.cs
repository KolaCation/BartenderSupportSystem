using System;

namespace BartenderSupportSystem.Server.DbModels
{
    internal sealed class BartenderDbModel
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public double Experience { get; private set; }
        public string PhotoPath { get; private set; }

        public BartenderDbModel(Guid id, string firstName, string lastName, double experience, string photoPath)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Experience = experience;
            PhotoPath = photoPath;
        }
    }
}
