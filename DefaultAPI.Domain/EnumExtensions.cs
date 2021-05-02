using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DefaultAPI.Domain.Enums
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            return value
                .GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DisplayAttribute>(false)
                ?.Name
                ?? value.ToString();
        }

        public static string GetDisplayShortName(this Enum value)
        {
            return value
                .GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DisplayAttribute>(false)
                ?.ShortName
                ?? value.ToString();
        }

        public static T[] GetEnumValues<T>() where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("GetValues<T> can only be called for types derived from System.Enum", "T");

            return (T[])Enum.GetValues(typeof(T));
        }
    }
}
