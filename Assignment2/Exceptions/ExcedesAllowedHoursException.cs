using System;

namespace WebApplication1.Exceptions
{
    public class ExcedesAllowedHoursException : Exception
    {
        private string errorMessage { get; set; }

        public ExcedesAllowedHoursException()
        {
            errorMessage = "Hours to be allocated is exceding the allowed hours";
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