using System;

namespace WebApplication1.Exceptions
{
    public class InterventionAdditionConflictExpection : Exception
    {
        private string errorMessage { get; set; }

        public InterventionAdditionConflictExpection()
        {
            errorMessage = "There are some conflictions about Intervention Addition";
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