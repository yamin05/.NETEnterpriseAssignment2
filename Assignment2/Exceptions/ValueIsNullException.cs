using System;

namespace WebApplication2.Exceptions
{
    public class ValueIsNullException : Exception
    {
        private string errorMessage { get; set; }

        public ValueIsNullException()
        {
            errorMessage = "The value cannot be null";
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