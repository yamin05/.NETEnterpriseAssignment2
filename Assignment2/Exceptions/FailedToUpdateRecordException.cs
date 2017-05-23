using System;

namespace WebApplication2.Exceptions
{
    public class FailedToUpdateRecordException : Exception
    {
        private string errorMessage { get; set; }

        public FailedToUpdateRecordException()
        {
            errorMessage = "Failed to update a record in database";
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