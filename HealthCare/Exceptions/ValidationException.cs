using System;
using System.Runtime.Serialization;

namespace HealthCare.Exceptions
{
    internal class ValidationException : Exception
    {
        public ValidationException() : this("Uneseni neispravni podaci. Pokušajte ponovo.") { }

        public ValidationException(string? message) : base(message) { }

        public ValidationException(string? message, Exception? innerException) : base(message, innerException) { }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
