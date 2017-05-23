using System;

namespace WebApplication2.Exceptions
{
    public class WrongInputPasswordException : Exception
    {
        private string errorMessage { get; set; }

        public WrongInputPasswordException()
        {
            errorMessage = "The user password doesn't match with the database";
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