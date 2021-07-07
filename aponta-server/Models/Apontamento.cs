using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Models
{
    public class Apontamento
    {
        private int _id;
        private DateTime _dataHoraInicial;
        private DateTime _dataHoraFinal;
        private string _descricao;
        private Login _usuario;
        private Tarefa _tarefa;

        public Apontamento()
        {
        }

        public Apontamento(int id, DateTime dataHoraInicial, DateTime dataHoraFinal, 
            string descricao, Login usuario, Tarefa tarefa)
        {
            ID = id;
            DATA_HORA_INICIAL = dataHoraInicial;
            DATA_HORA_FINAL = dataHoraFinal;
            DESCRICAO = descricao;
            USUARIO = usuario;
            TAREFA = tarefa;
        }

        public int ID { get => _id; set => _id = value; }
        public DateTime DATA_HORA_INICIAL { get => _dataHoraInicial; set => _dataHoraInicial = value; }
        public DateTime DATA_HORA_FINAL { get => _dataHoraFinal; set => _dataHoraFinal = value; }
        public string DESCRICAO { get => _descricao; set => _descricao = value; }
        public Login USUARIO { get => _usuario; set => _usuario = value; }
        public Tarefa TAREFA { get => _tarefa; set => _tarefa = value; }

     }
}
