using apontaServer.Models;
using apontaServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace apontaServer.Services
{
    public class ApontamentoService
    {
        private IApontamentoRepositorio repositorio;
        private TarefaService tarefa;
        private LoginService usuario;
        private TempoTotalTarefaService tempo;
        private MetodosGenericosService metodos;

        public ApontamentoService(IApontamentoRepositorio repositorio, TarefaService tarefa, 
            LoginService usuario, TempoTotalTarefaService tempo, MetodosGenericosService metodos)
        {
            this.repositorio = repositorio;
            this.tarefa = tarefa;
            this.usuario = usuario;
            this.tempo = tempo;
            this.metodos = metodos;
        }

        public ActionResult<dynamic> DefinirApontamento(Apontamento apontamento)
        {
            try
            {
                var tarefaDb = tarefa.BuscarTarefa(apontamento.TAREFA.ID_TAREFA_CHAMADO, apontamento.TAREFA, true);

                apontamento.TAREFA = tarefaDb;

                var apontamentoDb = repositorio.Get(apontamento.USUARIO.ID_USUARIO, apontamento.TAREFA.ID_TAREFA);

                if(apontamentoDb != null)
                {
                    if(apontamentoDb.DATA_HORA_FINAL.Year == 1) 
                    {
                        return new
                        {
                            message = "Já existe um apontamento em andamento para essa tarefa"
                        };
                    }
                }

                var ultimoApontamentoDb = UltimoApontamentoUsuario(apontamento.USUARIO.ID_USUARIO).Value;

                if (ultimoApontamentoDb.apontamento.ID != 0 && ultimoApontamentoDb.apontamento.DATA_HORA_FINAL.ToString() == "01/01/0001 00:00:00")
                {
                    DefinirPausa(ultimoApontamentoDb.apontamento.ID);
                }

                apontamento.DATA_HORA_INICIAL = metodos.DataHoraBrasilia();

                apontamento.TAREFA = tarefa.CadastrarTarefa(apontamento.TAREFA);

                repositorio.Post(apontamento);

                return UltimoApontamentoUsuario(apontamento.USUARIO.ID_USUARIO);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DefinirPausa(int id)
        {
            try
            {
                var apontamento = repositorio.Get(id);

                if(apontamento == null)
                {
                    throw new Exception(String.Format("O apontamento {0} não existe", id));
                }

                var idUsuario = metodos.Dlookup("ID_USUARIO", "T_APONTAMENTO", String.Format("ID = {0}", apontamento.ID));
                var idTarefa = metodos.Dlookup("ID_TAREFA", "T_APONTAMENTO", String.Format("ID = {0}", apontamento.ID));

                apontamento.USUARIO = usuario.BuscarLogin(Convert.ToInt32(idUsuario));
                apontamento.TAREFA = tarefa.BuscarTarefa(Convert.ToInt32(idTarefa), null, false);
                apontamento.DATA_HORA_FINAL = metodos.DataHoraBrasilia();

                repositorio.Put(id, apontamento.DATA_HORA_FINAL);

                tempo.CalcularTempoTotalTarefa(apontamento);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AlterarTarefaApontamento(int id, Tarefa newTarefa)
        {
            var apontamento = repositorio.Get(id);

            if(apontamento == null)
            {
                throw new Exception(String.Format("O apontamento {0} não existe", id));
            }

            var tarefaDb = tarefa.BuscarTarefa(newTarefa.ID_TAREFA_CHAMADO, null, false);

            if(tarefaDb == null)
            {
                throw new Exception(String.Format("A tarefa {0} não existe", newTarefa.ID_TAREFA_CHAMADO));
            }

            repositorio.Put(apontamento.ID, tarefaDb.ID_TAREFA);
        }

        public List<Apontamento> ListarApontamentoUsuario(int id)
        {
            try
            {
                var usuario = this.usuario.BuscarLogin(id);

                var listaApontamento = repositorio.List(usuario);

                foreach(Apontamento ap in listaApontamento)
                {
                    ap.USUARIO = usuario;
                    var idTarefa = metodos.Dlookup("ID_TAREFA", "T_APONTAMENTO", String.Format("ID = {0}", ap.ID));
                    ap.TAREFA = tarefa.BuscarTarefa(Convert.ToInt32(idTarefa), null, false);
                }

                return listaApontamento;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Apontamento> ListarApontamentoTarefa(int id)
        {
            try
            {
                var tarefa = this.tarefa.BuscarTarefa(id, null, false);

                var listaApontamento = repositorio.List(tarefa);

                foreach (Apontamento ap in listaApontamento)
                {
                    ap.TAREFA = tarefa;
                    var idUsuario = metodos.Dlookup("ID_USUARIO", "T_APONTAMENTO", String.Format("ID = {0}", ap.ID));
                    ap.USUARIO = this.usuario.BuscarLogin(Convert.ToInt32(idUsuario));
                }

                return listaApontamento;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult<dynamic> UltimoApontamentoUsuario(int id)
        {
            try
            {
                var usuario = this.usuario.BuscarLogin(id);

                var apontamento = repositorio.Get(usuario);

                if(apontamento == null)
                {
                    return new 
                    {
                        apontamento = new Apontamento(),
                        message = "Usuário não possui apontamentos" 
                    };
                }

                apontamento.USUARIO = usuario;
                var idTarefa = metodos.Dlookup("ID_TAREFA", "T_APONTAMENTO", String.Format("ID = {0}", apontamento.ID));
                apontamento.TAREFA = this.tarefa.BuscarTarefa(Convert.ToInt32(idTarefa), null, false);

                return new
                {
                    apontamento = apontamento
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult<dynamic> ListarApontatamentoUsuarioPaginado(int id, int quatidadePagina, int paginaAtual)
        {

            try
            {
                var usuario = this.usuario.BuscarLogin(id);

                var where = String.Format(@"ID_USUARIO = {0} AND DATA_HORA_FINAL IS NOT NULL", id);
                var quatidadeRegistrosTabela = metodos.Dlookup("COUNT(ID)", "T_APONTAMENTO", where);

                var offset = quatidadePagina * paginaAtual; 

                var listaApontamento = repositorio.List(usuario, quatidadePagina, offset);

                foreach(Apontamento ap in listaApontamento)
                {
                    ap.USUARIO = usuario;
                    var idTarefa = metodos.Dlookup("ID_TAREFA", "T_APONTAMENTO", String.Format("ID = {0}", ap.ID));
                    ap.TAREFA = tarefa.BuscarTarefa(Convert.ToInt32(idTarefa), null, false);
                }

                return new
                {
                    listaApontamento = listaApontamento,
                    total = quatidadeRegistrosTabela,
                    paginaAtual = paginaAtual
                };
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
