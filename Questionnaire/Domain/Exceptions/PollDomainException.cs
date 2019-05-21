using System;

namespace Questionnaire.Domain.Exceptions
{
    public class PollDomainException : Exception
    {
        public PollDomainException()
        { }

        public PollDomainException(string message)
            : base(message)
        { }

        public PollDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}