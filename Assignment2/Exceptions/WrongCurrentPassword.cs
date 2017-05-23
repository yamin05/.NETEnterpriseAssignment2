using System;

namespace WebApplication2.Exceptions
{
    public class WrongCurrentPassword : Exception
    {
        private string errorMessage { get; set; }

        public WrongCurrentPassword()
        {
            errorMessage = "The Curent Password doesn't match the database";
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