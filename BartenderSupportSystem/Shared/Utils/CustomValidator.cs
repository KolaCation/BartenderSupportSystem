using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Shared.Utils
{
    public static class CustomValidator
    {
        public static void ValidateObject<T>(T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
        }

        public static void ValidateNumber(double number, int minVal, int maxVal)
        {
            if (number < minVal || number > maxVal) throw new ArgumentException(nameof(number));
        }

        public static void ValidateNumber(int number, int minVal, int maxVal)
        {
            if (number < minVal || number > maxVal) throw new ArgumentException(nameof(number));
        }

        public static void ValidateString(string str, int minLength, int maxLength, bool nullable = false)
        {
            if (nullable == false)
            {
                if (string.IsNullOrEmpty(str)) throw new ArgumentNullException(nameof(str));
            }

            if (str.Length < minLength || str.Length > maxLength) throw new ArgumentException(nameof(str));
        }

        public static void ValidateId(Guid id)
        {
            if (id.Equals(Guid.Empty)) throw new ArgumentException(nameof(id));
        }
    }
}