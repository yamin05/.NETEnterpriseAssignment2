using System;

namespace WebApplication2.Exceptions
{
    public class InvalidEmailFormatException : Exception
    {
        private string errorMessage { get; set; }

        public InvalidEmailFormatException()
        {
            errorMessage = "The format of the email address is not valid";
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