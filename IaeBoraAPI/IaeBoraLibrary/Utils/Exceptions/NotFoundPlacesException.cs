using System;

namespace IaeBoraLibrary.Utils.Exceptions
{
    public class NotFoundPlacesException : Exception
    {
        public NotFoundPlacesException(string message) : base(message) { }
    }
}