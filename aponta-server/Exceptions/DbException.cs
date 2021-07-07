using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Exceptions
{
    public class DbException : Exception
    {
        public DbException(string msg) : base(msg)
        {
        }
    }
}
