using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public string? Error { get; set; }
        public string[]? Errors { get; set; }

        public BadRequestException(string message, string error) : base(message)
        {
            Error = error;
        }

        public BadRequestException(string message, string[] errors) : base(message)
        {
            Errors = errors;
        }

    }
}
