using System;

namespace IaeBoraLibrary.Utils.Exceptions
{
    class UserServiceException : Exception
    {
        public UserServiceException(string message) : base(message) { }
    }
}
