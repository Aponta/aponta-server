using apontaServer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Repositories
{
    public interface IMetodosGenericosRepositorio
    {
        string Dlookup(string campoBuscado, string tabela, string where);

        void StartTransactionCommitRollback(MetodosGenericosEnum metodosGenericos);

    }
}
    