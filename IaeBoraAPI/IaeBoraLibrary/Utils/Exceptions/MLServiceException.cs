using System;

namespace IaeBoraLibrary.Utils.Exceptions
{
    public class MLServiceException : Exception
    {
        public MLServiceException(string message) : base(message) { }
        public MLServiceException(string message, Exception inner) : base(message, inner) { }
    }
}
