using apontaServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Repositories
{
    public interface IApontamentoRepositorio
    {
        Apontamento Get(int id);
        
        Apontamento Get(int idUsuario, int IdTarefa);

        Apontamento Get(Login usuario);

        List<Apontamento> List(Login usuario);

        List<Apontamento> List(Login usuario, int quantidadeRegistros, int offset);

        List<Apontamento> List(Tarefa tarefa, int quantidadeRegistros, int offset);

        Apontamento Post(Apontamento apontamento);

        void Put(int id, DateTime dataHoraFinal);

        void Put(int id, int idTarefa);

        void Delete(int id);
    }
}
