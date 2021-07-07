using apontaServer.Models;
using apontaServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontaServer.Controllers
{
    [Route("aponta/[controller]")]
    [ApiController]

    public class TempoTotalTarefaController : ControllerBase
    {
        private TempoTotalTarefaService tempoTotalTarefaService;

        public TempoTotalTarefaController(TempoTotalTarefaService tempoTotalTarefaService)
        {
            this.tempoTotalTarefaService = tempoTotalTarefaService;
        }


        [HttpGet("{idUsuario}/{idTarefa}")]
        [Authorize]
        public TempoTotalTarefa Get(int idUsuario, int idTarefa)
        {
            return tempoTotalTarefaService.BuscarTempoTotalTarefa(idUsuario, idTarefa);
        }
    }
}
