using System;

namespace WebApplication1.Exceptions
{
    public class CannotEditStatusException : Exception
    {
        private string errorMessage { get; set; }

        public CannotEditStatusException()
        {
            errorMessage = "The status is not editable from the current state";
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