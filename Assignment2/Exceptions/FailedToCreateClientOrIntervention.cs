using System;

namespace WebApplication2.Exceptions
{
    public class FailedToCreateClientOrIntervention : Exception
    {
        private string errorMessage { get; set; }

        public FailedToCreateClientOrIntervention()
        {
            errorMessage = "Failed to create a client/Intervention in database";
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