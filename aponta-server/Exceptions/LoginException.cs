using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaserver.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException(string msg) : base(msg)
        {

        }
    }
}
