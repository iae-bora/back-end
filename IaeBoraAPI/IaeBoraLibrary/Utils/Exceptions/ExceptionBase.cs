using System;

namespace IaeBoraLibrary.Utils.Exceptions
{
    public class ExceptionBase : Exception
    {
        public ExceptionBase(string message) : base(message) { }
        public ExceptionBase(string message, Exception inner) : base(message, inner) { }
    }
}
