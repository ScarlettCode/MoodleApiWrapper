using System;

namespace MoodleApiWrapper.Exceptions
{
    public class MoodleException : Exception
    {
        public string ResponseText { get; private set; }
        public string RequestedPath { get; private set; }

        public Error Error { get; private set; }

        public MoodleException(string responseText, string requestedPath, Error error)
        {
            ResponseText = responseText;
            RequestedPath = requestedPath;
            Error = error;
        }
    }
}
