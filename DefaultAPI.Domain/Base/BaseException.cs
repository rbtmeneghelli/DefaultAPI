using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Base
{
    public abstract class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {

        }
    }
}
