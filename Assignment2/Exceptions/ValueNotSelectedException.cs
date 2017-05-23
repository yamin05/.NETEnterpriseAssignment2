﻿using System;

namespace WebApplication2.Exceptions
{
    public class ValueNotSelectedException : Exception
    {
        private string errorMessage { get; set; }

        public ValueNotSelectedException()
        {
            errorMessage = "Please select a value first";
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