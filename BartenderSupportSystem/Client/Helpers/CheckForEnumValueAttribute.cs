using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Client.Helpers
{
    public class CheckForEnumValueAttribute : ValidationAttribute
    {
        private readonly Type _enumType;
        private string _name;

        public CheckForEnumValueAttribute(Type enumType)
        {
            _enumType = enumType;
        }
        public override string FormatErrorMessage(string name)
        {
            return $"{_name} can not be found.";
        }

        public override bool IsValid(object value)
        {
            if (value == null) return false;
            var name = value.ToString();
            _name = name;
            var enumNamesList = Enum.GetNames(_enumType).ToList();
            if (enumNamesList.Contains(name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
