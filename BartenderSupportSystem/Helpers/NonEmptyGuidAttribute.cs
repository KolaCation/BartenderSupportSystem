using System;
using System.ComponentModel.DataAnnotations;

namespace BartenderSupportSystem.Server.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class NonEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is Guid guidValue && guidValue.Equals(Guid.Empty))
            {
                return new ValidationResult("Guid cannot be empty.");
            }
            else
            {
                return null;
            }
        }
    }
}
