using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class ResultReturned
    {
        public string Message { get; set; }
        public object Result { get; set; }

        public ResultReturned()
        {

        }

        public ResultReturned(string message, object model)
        {
            Message = message;
            Result = model;
        }
    }
}
