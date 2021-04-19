using System;
using System.Runtime.Serialization;

namespace Lift.Exceptions
{
    [Serializable]
    internal class AlreadyOnDestinationException : Exception
    {
        public AlreadyOnDestinationException()
        {
        }

        public AlreadyOnDestinationException(string message) : base(message)
        {
        }

        public AlreadyOnDestinationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyOnDestinationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
