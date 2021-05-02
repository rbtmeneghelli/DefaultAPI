using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class ResultReturned
    {
        public bool Result { get; set; }
        public string Message { get; set; }

        public ResultReturned()
        {

        }

        public ResultReturned(bool result, string message)
        {
            Result = result;
            Message = message;
        }
    }
}
