using System;

namespace WebApplication1.Exceptions
{
    public class WrongUserInputException : Exception
    {
        private string errorMessage { get; set; }

        public WrongUserInputException()
        {
            errorMessage = "The user input doesn't match with the database";
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