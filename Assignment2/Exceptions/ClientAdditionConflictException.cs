using System;

namespace WebApplication1.Exceptions
{
    public class ClientAdditionConflictException : Exception
    {
        private string errorMessage { get; set; }

        public ClientAdditionConflictException()
        {
            errorMessage = "There are some conflictions about Client Addition";
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