using apontaServer.Models;
using apontaServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apontasServer.Controllers
{
    [Route("aponta/[controller]")]
    [ApiController]

    public class TarefaController : Controller
{
        private TarefaService tarefaService;

        public TarefaController(TarefaService tarefaService)
        {
            this.tarefaService = tarefaService;
        }

        [HttpGet("{idTarefa}")]
        [Authorize]
        public ActionResult<dynamic> GetTarefa(int idTarefa)
        {
            return tarefaService.ListTarefa(idTarefa);
        }
    }
}
