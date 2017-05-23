using System;

namespace WebApplication2.Exceptions
{
    public class FailedToCreateRecordException : Exception
    {
        private string errorMessage { get; set; }

        public FailedToCreateRecordException()
        {
            errorMessage = "Failed to create a new record in database";
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