using apontaServer.Models;
using apontaServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace apontaServer.Controllers
{
    [Route("aponta/[controller]")]
    [ApiController]

    public class ApontamentoController : ControllerBase
    {
        private ApontamentoService apontamentoService;

        public ApontamentoController(ApontamentoService apontamentoService)
        {
            this.apontamentoService = apontamentoService;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<dynamic> Post([FromBody] Apontamento apontamento)
        {
            try
            {
                return apontamentoService.DefinirApontamento(apontamento);
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<dynamic> Put(int id)
        {
            try
            {
                apontamentoService.DefinirPausa(id);
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch(Exception erro)
            {
                return StatusCode((int)HttpStatusCode.FailedDependency, erro.Message);
            }
        }

        [HttpPut("{id}/{idTarefaChamado}")]
        [Authorize]
        public ActionResult<dynamic> Put(int id, [FromBody] Tarefa tarefa)
        {
            try
            {
                apontamentoService.AlterarTarefaApontamento(id, tarefa);
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch(Exception erro)
            {
                return StatusCode((int)HttpStatusCode.FailedDependency, erro.Message);
            }
        }

        [HttpGet("usuario/{id}")]
        [Authorize]
        public List<Apontamento> GetUsuario(int id)
        {
            return apontamentoService.ListarApontamentoUsuario(id);
        }

        [HttpGet("tarefa/{id}")]
        [Authorize]
        public List<Apontamento> GetTarefa(int id)
        {
            return apontamentoService.ListarApontamentoTarefa(id);
        }

        [HttpPost("paginado")]
        [Authorize]
        public dynamic GetApontamentoPaginado([FromBody] dynamic obj)
        {
            return apontamentoService.ListarApontatamentoUsuarioPaginado(
                obj.GetProperty("id").GetInt32(),
                obj.GetProperty("quantidadePagina").GetInt32(),
                obj.GetProperty("paginaAtual").GetInt32()
                );
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<dynamic> Get(int id)
        {
            return apontamentoService.UltimoApontamentoUsuario(id);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<dynamic> Delete(int id)
        {
            return apontamentoService.ExluirApontamento(id);
        }
    }
}
