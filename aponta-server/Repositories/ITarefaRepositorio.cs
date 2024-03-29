﻿using apontaServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Repositories
{
    public interface ITarefaRepositorio
    {
        Tarefa Get(int id);

        Tarefa GetTarefa(int idTarefa);

        List<Tarefa> GetTarefa(int idUsuario, string termo);

        Tarefa Post(Tarefa tarefa);

        List<Tarefa> List(int idTarefa, int idUsuario);
    }
}
