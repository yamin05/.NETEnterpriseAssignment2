using System;

namespace WebApplication1.Exceptions
{
    public class InvalidPasswordFormatException : Exception
    {
        private string errorMessage { get; set; }

        public InvalidPasswordFormatException()
        {
            errorMessage = "The format of the password is not valid";
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