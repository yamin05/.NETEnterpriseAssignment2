using System;

namespace WebApplication2.Exceptions
{
    public class InvalidConditionLimitException : Exception
    {
        private string errorMessage { get; set; }

        public InvalidConditionLimitException()
        {
            errorMessage = "The Condition Cannot Excede 100%";
        }

        public override string Message
        {
            get
            {
                return errorMessage;
            }
        }
    }
}