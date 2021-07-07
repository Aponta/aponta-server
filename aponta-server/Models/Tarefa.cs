using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Models
{
    public class Tarefa
    {
        private int _id;
        private int _idTarefaChamado;
        private string _clienteTarefa;

        public Tarefa()
        {

        }
        public Tarefa(int id, int idTarefaChamado, string clienteTarefa)
        {
            ID_TAREFA = id;
            ID_TAREFA_CHAMADO = idTarefaChamado;
            CLIENTE_TAREFA = clienteTarefa;
        }

        public int ID_TAREFA { get => _id; set => _id = value; }
        public int ID_TAREFA_CHAMADO { get => _idTarefaChamado; set => _idTarefaChamado = value; }
        public string CLIENTE_TAREFA { get => _clienteTarefa; set => _clienteTarefa = value; }
    }
}
