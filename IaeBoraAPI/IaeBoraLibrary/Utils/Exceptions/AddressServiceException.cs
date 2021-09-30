using System;

namespace IaeBoraLibrary.Utils.Exceptions
{
    public class AddressServiceException : Exception
    {
        public AddressServiceException(string message) : base(message) { }
    }
}
