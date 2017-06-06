using System;

namespace WebApplication2.Exceptions
{
    public class NoClientCreatedException : Exception
    {
        private string errorMessage { get; set; }

        public NoClientCreatedException()
        {
            errorMessage = "No Client Created Yet. Please Create a Client First";
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