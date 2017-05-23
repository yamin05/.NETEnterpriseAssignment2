using System;

namespace WebApplication2.Exceptions
{
    public class EditQMInfoPermissionException : Exception
    {
        private string errorMessage { get; set; }

        public EditQMInfoPermissionException()
        {
            errorMessage = "You don't have enough permission to change the Quality Management Information of this intervention";
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