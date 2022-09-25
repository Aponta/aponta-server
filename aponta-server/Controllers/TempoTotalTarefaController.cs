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


        [HttpPost("usuario")]
        [Authorize]
        public dynamic GetTempoTotalTarefa([FromBody] dynamic obj)
        {
            return tempoTotalTarefaService.ListarTempoTotalTarefaPaginado(
                Int32.Parse(obj.GetProperty("idUsuario").GetString()),
                obj.GetProperty("quantidadePagina").GetInt32(),
                obj.GetProperty("paginaAtual").GetInt32()
                );
        }

        [HttpPost("termo")]
        [Authorize]
        public dynamic GetTempoTotalTarefaPorTarefa([FromBody] dynamic obj)
        {
            return tempoTotalTarefaService.ListarTempoTotalTarefaPaginadoPorTermo(
                Int32.Parse(obj.GetProperty("idUsuario").GetString()),
                obj.GetProperty("termoFiltro").GetString(),
                obj.GetProperty("quantidadePagina").GetInt32(),
                obj.GetProperty("paginaAtual").GetInt32()
                );
        }
    }
}
