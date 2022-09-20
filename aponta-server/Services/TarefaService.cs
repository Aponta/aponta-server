using apontaServer.Models;
using apontaServer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Services
{
    public class TarefaService
    {
        private ITarefaRepositorio repositorio;

        public TarefaService(ITarefaRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public Tarefa CadastrarTarefa(Tarefa tarefa)
        {
            try
            {
                Tarefa tarefaDb = repositorio.Get(tarefa.ID_TAREFA_CHAMADO);

                if(tarefaDb != null)
                {
                    return tarefaDb;
                }

                return repositorio.Post(tarefa);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Tarefa BuscarTarefa(int id, Tarefa tarefa, bool cadastrar)
        {
            try
            {
                Tarefa tarefaDb = repositorio.Get(id);

                if(tarefaDb == null && cadastrar)
                {
                    return CadastrarTarefa(tarefa);
                }

                return tarefaDb;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult<dynamic> ListTarefa(int idTarefa, int idUsuario)
        {
            try
            {
                List<Tarefa> list = repositorio.List(idTarefa, idUsuario);

                if(list.Count == 0)
                {
                    return new
                    {
                        message = "Nenhuma tarefa encontrada"
                    };
                }
                    
                return list;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
