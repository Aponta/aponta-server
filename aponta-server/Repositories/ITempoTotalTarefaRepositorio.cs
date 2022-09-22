using apontaServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Repositories
{
    public interface ITempoTotalTarefaRepositorio
    {
        void Post(TempoTotalTarefa tempoTotalTarefa);

        void Put(int id, long tempo);

        TempoTotalTarefa Get(int idTarefa);

        List<dynamic> List(Login usuario, int quantidadeRegistros, int offset);

        List<dynamic> List(List<Tarefa> tarefas, int quantidadeRegistros, int offset);
    }
}
