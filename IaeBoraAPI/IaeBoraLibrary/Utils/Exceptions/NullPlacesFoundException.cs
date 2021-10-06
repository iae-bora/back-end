using System;

namespace IaeBoraLibrary.Utils.Exceptions
{
    public class NullPlacesFoundException : Exception
    {
        public NullPlacesFoundException(string message) : base(message) { }
    }
}
