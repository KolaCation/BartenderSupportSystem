using BartenderSupportSystem.Shared.Utils;
using System;

namespace BartenderSupportSystem.Server.Data.DbModels
{
    internal sealed class BartenderDbModel
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public BartenderDbModel(string firstName, string lastName)
        {
            CustomValidator.ValidateString(firstName, CustomValidatorDefaultValues.StrDefaultMinLength, CustomValidatorDefaultValues.StrDefaultMaxLength);
            CustomValidator.ValidateString(lastName, CustomValidatorDefaultValues.StrDefaultMinLength, CustomValidatorDefaultValues.StrDefaultMaxLength);
            FirstName = firstName;
            LastName = lastName;
        }

        public BartenderDbModel(int id, string firstName, string lastName) : this(firstName, lastName)
        {
            Id = id;
        }
    }
}
