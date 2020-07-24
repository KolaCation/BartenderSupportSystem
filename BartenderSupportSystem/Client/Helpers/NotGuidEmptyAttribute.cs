using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Client.Helpers
{
    public class NotGuidEmptyAttribute : ValidationAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Select a type.";
        }

        public override bool IsValid(object value)
        {
            var guidValue = Guid.Parse(value.ToString());
            if (guidValue.Equals(Guid.Empty))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
