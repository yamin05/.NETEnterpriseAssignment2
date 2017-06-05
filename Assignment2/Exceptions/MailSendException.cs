using System;

namespace WebApplication2.Exceptions
{
    public class MailSendException : Exception
    {
        private string errorMessage { get; set; }

        public MailSendException()
        {
            errorMessage = "Cannot send mail";
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