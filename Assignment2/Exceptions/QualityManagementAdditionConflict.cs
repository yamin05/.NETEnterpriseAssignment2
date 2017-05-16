using System;

namespace WebApplication1.Exceptions
{
    public class QualityManagementAdditionConflict : Exception
    {
        private string errorMessage { get; set; }

        public QualityManagementAdditionConflict()
        {
            errorMessage = "There are some conflictions about Quality Management Addition";
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