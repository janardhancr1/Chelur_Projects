using System;

namespace PALibrary.Library.Exception
{
    public class PAException : ApplicationException
    {
        public PAException()
        {
        }

        public PAException(string message)
            : base(message)
        {
        }
    }
}
