using System;

namespace WebApplication1.Exceptions
{
    public class FailedToUpdateClientListOrInterventionList : Exception
    {
        private string errorMessage { get; set; }

        public FailedToUpdateClientListOrInterventionList()
        {
            errorMessage = "Failed to Update client/Intervention list";
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