using System;

namespace WebApplication1.Exceptions
{
    public class WrongInputEmailException : Exception
    {
        private string errorMessage { get; set; }

        public WrongInputEmailException()
        {
            errorMessage = "The user email doesn't match with the database";
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