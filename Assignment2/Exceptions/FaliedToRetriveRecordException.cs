using System;

namespace WebApplication2.Exceptions
{
    public class FaliedToRetriveRecordException: Exception
    {
        private string errorMessage { get; set; }

        public FaliedToRetriveRecordException()
        {
            errorMessage = "Failed to retrive record from database";
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