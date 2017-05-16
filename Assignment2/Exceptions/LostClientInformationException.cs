using System;

namespace WebApplication1.Exceptions
{
    public class LostClientInformationException : Exception
    {
        private string errorMessage { get; set; }

        public LostClientInformationException()
        {
            errorMessage = "Lost the information of Client";
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