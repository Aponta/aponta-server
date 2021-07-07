using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Models
{
    public class TempoTotalTarefa
    {
        private int _id;
        private long _tempoTotal;
        private Login _usuario;
        private Tarefa _tarefa;

        public TempoTotalTarefa()
        {
        }

        public TempoTotalTarefa(int id, long tempoTotal, Login usuario, Tarefa tarefa)
        {
            ID = id;
            TEMPO_TOTAL = tempoTotal;
            USUARIO = usuario;
            TAREFA = tarefa;
        }

        public int ID { get => _id; set => _id = value; }
        public long TEMPO_TOTAL { get => _tempoTotal; set => _tempoTotal = value; }
        public Login USUARIO { get => _usuario; set => _usuario = value; }
        public Tarefa TAREFA { get => _tarefa; set => _tarefa = value; }
    }
}
