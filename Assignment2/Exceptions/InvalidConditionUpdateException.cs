using System;

namespace WebApplication2.Exceptions
{
    public class InvalidConditionUpdateException : Exception
    {
        private string errorMessage { get; set; }

        public InvalidConditionUpdateException()
        {
            errorMessage = "The Condition Cannot Excede The Previos Update";
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