using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public sealed class ApiLogItem
    {
        public long Id { get; set; }

        [Required]
        public DateTime RequestTime { get; set; }

        [Required]
        public long ResponseMillis { get; set; }

        [Required]
        public int StatusCode { get; set; }

        [Required]
        public string Method { get; set; }

        [Required]
        public string Path { get; set; }

        public string QueryString { get; set; }

        public string RequestBody { get; set; }

        public string ResponseBody { get; set; }

        public ApiLogItem(DateTime requestTime, long responseMillis, int statusCode, string method, string path, string queryString, string requestBody, string responseBody)
        {
            RequestTime = requestTime;
            ResponseMillis = responseMillis;
            StatusCode = statusCode;
            Method = method;
            Path = path;
            QueryString = queryString;
            RequestBody = requestBody;
            RequestTime = requestTime;
        }
    }
}
