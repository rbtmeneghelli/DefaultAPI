using System;

namespace DefaultAPI.Infra.CrossCutting
{
    public static class GuardClauses
    {
        public static void IsNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue is null)
                throw new ArgumentNullException(argumentName);
        }

        public static void IsNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
                throw new ArgumentNullException(argumentName);
        }

        public static void IsNotZero(int argumentValue, string argumentName)
        {
            if (argumentValue == 0)
                throw new ArgumentException($"Argumento '{argumentName}' não pode ser zero");
        }

        public static void IsNotSmallerThan(int maxValue, int argumentValue, string argumentName)
        {
            if (argumentValue >= maxValue)
                throw new ArgumentException($"Argumento '{argumentName}' não pode exceder '{maxValue}'");
        }

        public static void IsNotBiggerThan(int minValue, int argumentValue, string argumentName)
        {
            if (argumentValue <= minValue)
                throw new ArgumentException($"Argumento '{argumentName}' não pode ser menor que '{minValue}'");
        }
    }
}
