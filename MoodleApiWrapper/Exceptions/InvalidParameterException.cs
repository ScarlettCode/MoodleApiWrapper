using System;
using System.Collections.Generic;
using System.Text;

namespace MoodleApiWrapper.Exceptions
{
    public class InvalidParameterException : MoodleException
    {
        public InvalidParameterException(string responseText, string requestedPath, Error error) : base(responseText, requestedPath, error)
        {
        }
    }
}
