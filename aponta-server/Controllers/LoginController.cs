using apontaServer.Models;
using apontaServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace apontaServer.Controllers
{
    [Route("aponta/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService loginService;

        public LoginController(LoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        [Route("logar")]
        [AllowAnonymous]
        public ActionResult<dynamic> Get([FromBody] Login usuario)
        {
            return loginService.Logar(usuario.USUARIO, usuario.retornarSenha());
        }

        [HttpPost]
        [Route("cadastrar")]
        [AllowAnonymous]
        public ActionResult<dynamic> Post([FromBody] Login usuario)
        {
            return loginService.CadastrarLogin(usuario.USUARIO, usuario.retornarSenha());
        }

        [HttpGet]
        [Route("logado")]
        [Authorize]
        public ActionResult<bool> Get()
        {
            return true;
        }
    }
}
