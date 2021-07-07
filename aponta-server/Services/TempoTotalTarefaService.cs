using apontaServer.Repositories;
using apontaServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Services
{
    public class TempoTotalTarefaService
    {
        private ITempoTotalTarefaRepositorio repositorio;
        private TarefaService tarefa;
        private LoginService usuario;


        public TempoTotalTarefaService(ITempoTotalTarefaRepositorio repositorio, TarefaService tarefa, LoginService usuario)
        {
            this.repositorio = repositorio;
            this.tarefa = tarefa;
            this.usuario = usuario;
        }

        public void CalcularTempoTotalTarefa(Apontamento apontamento)
        {
            try
            {
                var diferenca = apontamento.DATA_HORA_FINAL.Subtract(apontamento.DATA_HORA_INICIAL).TotalMilliseconds;

                var tempoTotalTarefa = repositorio.Get(apontamento.USUARIO.ID_USUARIO, apontamento.TAREFA.ID_TAREFA);

                if(tempoTotalTarefa != null)
                {
                    repositorio.Put(tempoTotalTarefa.ID, Convert.ToInt64(diferenca));
                }
                else
                {
                    repositorio.Post(new TempoTotalTarefa(0, Convert.ToInt64(diferenca), apontamento.USUARIO, apontamento.TAREFA));
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public TempoTotalTarefa BuscarTempoTotalTarefa(int idUsuario, int IdTarefa)
        {
            try
            {
                var tempoTotalTarefa = repositorio.Get(idUsuario, IdTarefa);

                if(tempoTotalTarefa == null)
                {
                    throw new Exception("Sem total para essa tarefa");
                }

                tempoTotalTarefa.USUARIO = usuario.BuscarLogin(idUsuario);
                tempoTotalTarefa.TAREFA = tarefa.BuscarTarefa(IdTarefa, null, false);

                return tempoTotalTarefa;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
