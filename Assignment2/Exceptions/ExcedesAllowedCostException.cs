using System;

namespace WebApplication2.Exceptions
{
    public class ExcedesAllowedCostException : Exception
    {
        private string errorMessage { get; set; }

        public ExcedesAllowedCostException()
        {
            errorMessage = "Cost to be allocated is exceding the allowed Cost";
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