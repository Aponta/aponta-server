using apontaServer.Repositories;
using apontaServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace apontaServer.Services
{
    public class TempoTotalTarefaService
    {
        private ITempoTotalTarefaRepositorio repositorio;
        private TarefaService tarefa;
        private LoginService usuario;
        private MetodosGenericosService metodos;

        public TempoTotalTarefaService(ITempoTotalTarefaRepositorio repositorio, TarefaService tarefa, LoginService usuario, MetodosGenericosService metodos)
        {
            this.repositorio = repositorio;
            this.tarefa = tarefa;
            this.usuario = usuario;
            this.metodos = metodos;
        }

        public void CalcularTempoTotalTarefa(Apontamento apontamento)
        {
            try
            {
                var diferenca = apontamento.DATA_HORA_FINAL.Subtract(apontamento.DATA_HORA_INICIAL).TotalMilliseconds;

                var tempoTotalTarefa = repositorio.Get(apontamento.TAREFA.ID_TAREFA);

                if(tempoTotalTarefa != null)
                {
                    repositorio.Put(tempoTotalTarefa.ID, Convert.ToInt64(diferenca));
                }
                else
                {
                    repositorio.Post(new TempoTotalTarefa(0, Convert.ToInt64(diferenca), apontamento.TAREFA));
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
                var tempoTotalTarefa = repositorio.Get(IdTarefa);

                if(tempoTotalTarefa == null)
                {
                    throw new Exception("Sem total para essa tarefa");
                }

                tempoTotalTarefa.TAREFA = tarefa.BuscarTarefa(IdTarefa, null, false);

                return tempoTotalTarefa;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult<dynamic> ListarTempoTotalTarefaPaginadoPorUsuario(int idUsuario, int quatidadePagina, int paginaAtual)
        {
            try
            {
                var usuarioDb = usuario.BuscarLogin(idUsuario);

                if(usuarioDb == null)
                {
                    return new
                    {
                        message = "Ocorreu um erro"
                    };
                }

                var where = String.Format(@"T.ID_USUARIO = {0}", idUsuario);
                var quatidadeRegistrosTabela = metodos.Dlookup("COUNT(TTT.ID)", "T_TEMPO_TOTAL_TAREFA TTT INNER JOIN T_TAREFA T ON TTT.ID_TAREFA = T.ID", where);

                var offset = quatidadePagina * paginaAtual;

                return new
                {
                    listaTempoTotalTarefa = repositorio.List(usuarioDb, quatidadePagina, offset).Select(item =>
                    {
                        return new
                        {
                            id = item.ID,
                            TEMPO_TOTAL = item.TEMPO_TOTAL,
                            TAREFA = tarefa.BuscarTarefa(item.ID_TAREFA, null, false)
                        };
                    }),
                    total = quatidadeRegistrosTabela,
                    paginaAtual = paginaAtual
                };
            }
            catch(Exception)
            {
                throw;
            }
        }

        public ActionResult<dynamic> ListarTempoTotalTarefaPaginadoPorTermo(string termo, int quatidadePagina, int paginaAtual)
        {
            try
            {
                var tarefasDb = tarefa.BuscarTarefa(termo);

                if(tarefasDb.Count() == 0)
                {
                    return new
                    {
                        message = "Nada encontrado"
                    };
                }

                var teste = tarefasDb.Select(item => item.ID_TAREFA);

                var where = String.Format(@"ID_TAREFA IN ({0})", );
                var quatidadeRegistrosTabela = metodos.Dlookup("COUNT(TTT.ID)", "T_TEMPO_TOTAL_TAREFA", where);

                var offset = quatidadePagina * paginaAtual;

                return new
                {
                    listaTempoTotalTarefa = repositorio.List(tarefasDb, quatidadePagina, offset).Select(item =>
                    {
                        return new
                        {
                            id = item.ID,
                            TEMPO_TOTAL = item.TEMPO_TOTAL,
                            TAREFA = tarefa.BuscarTarefa(item.ID_TAREFA, null, false)
                        };
                    }),
                    total = quatidadeRegistrosTabela,
                    paginaAtual = paginaAtual
                };
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
