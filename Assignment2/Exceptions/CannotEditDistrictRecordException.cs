using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Exceptions
{
    public class CannotEditDistrictException : Exception
    {
        private string errorMessage { get; set; }

        public CannotEditDistrictException()
        {
            errorMessage = "The district cannot same as the previous one";
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