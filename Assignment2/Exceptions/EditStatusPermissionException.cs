using System;

namespace WebApplication1.Exceptions
{
    public class EditStatusPermissionException : Exception
    {
        private string errorMessage { get; set; }

        public EditStatusPermissionException()
        {
            errorMessage = "You don't have enough permission to change the status";
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