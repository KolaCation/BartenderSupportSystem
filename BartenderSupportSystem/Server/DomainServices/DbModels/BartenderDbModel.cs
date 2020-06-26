using System;

namespace BartenderSupportSystem.Server.DomainServices.DbModels
{
    internal sealed class BartenderDbModel
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhotoPath { get; private set; }

        public BartenderDbModel(Guid id, string firstName, string lastName, string photoPath)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhotoPath = photoPath;
        }
    }
}
