using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Services.Exceptions
{
    public class ServiceException : Exception
    {
        public string ParamName { get; } = "";

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string paramName, string message) : base(message)
        {
            ParamName = paramName;
        }
    }
}
