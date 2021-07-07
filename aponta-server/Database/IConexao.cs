using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace apontaServer.Database
{
    public interface IConexao
    {
        IDbConnection AbrirConexao();
    }
}
