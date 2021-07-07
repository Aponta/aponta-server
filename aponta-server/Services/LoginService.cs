using apontaserver.Exceptions;
using apontaServer.Models;
using apontaServer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace apontaServer.Services
{
    public class LoginService
    {
        private ILoginRepositorio repositorio;
        private TokenService tokenService;

        public LoginService(ILoginRepositorio repositorio, TokenService tokenService)
        {
            this.repositorio = repositorio;
            this.tokenService = tokenService;
        }

        public ActionResult<dynamic> Logar(string usuario, string senha)
        {
            try
            {
                string senhaBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(senha));

                Login login = repositorio.Get(usuario, senhaBase64);

                if (login == null)
                {
                    return new { message = "Usuário ou senha inválidos" };
                };

                var token = tokenService.GerarToken(login);
                return new
                {
                    usuario = login,
                    token = token
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult<dynamic> CadastrarLogin(string usuario, string senha)
        {
            try
            {
                Login login = repositorio.Get(usuario);

                if(login != null)
                {
                    return new 
                    { 
                        message = "Usuário já existe" 
                    };
                }

                string senhaBase64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(senha));

                return repositorio.Post(new Login(0, usuario, senhaBase64));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Login BuscarLogin(int id)
        {
            try
            {
                Login usuario = repositorio.Get(id);

                if(usuario == null)
                {
                    throw new Exception("usuario não encontrado, id: " + id);
                }

                return usuario;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
