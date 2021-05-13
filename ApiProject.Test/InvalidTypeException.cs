using System;

namespace ApiProject.Test
{
    public class InvalidSubtypeException : Exception
    {
        public InvalidSubtypeException(Type targetType)
        : base($"{targetType}'s instance has invalid subtype for testing")
        { }
    }
}