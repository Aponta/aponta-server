using Dapper;
using MySql.Data.MySqlClient;
using apontaServer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apontaServer.Models;
using apontaServer.Exceptions;

namespace apontaServer.Repositories
{
    public class LoginRepositorio : ILoginRepositorio
    {
        public IConexao Conexao;

        public LoginRepositorio(Conexao Conexao)
        {
            this.Conexao = Conexao;
        }

        public LoginRepositorio(IConexao conexao)
        {
            Conexao = conexao;
        }

        public Login Get(string usuario, string senha)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<Login>(@"SELECT *, ID AS ID_USUARIO FROM T_LOGIN WHERE
                        USUARIO = @usuario AND SENHA = @senha", new { usuario, senha });

                    return resposta.FirstOrDefault();
                }
            }
            catch (MySqlException e)
            {
                throw new DbException("Erro ao executar comando sql: " + e.ToString());
            }
        }

        public Login Get(string usuario)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<Login>(@"SELECT * FROM T_LOGIN WHERE
                        USUARIO = @usuario", new { usuario });

                    return resposta.FirstOrDefault();
                }
            }
            catch (MySqlException e)
            {
                throw new DbException("Erro ao executar comando sql: " + e.ToString());
            }
        }

        public Login Get(int id)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    var resposta = cn.Query<Login>(@"SELECT *, ID AS ID_USUARIO FROM T_LOGIN WHERE
                        ID = @id", new { id });

                    return resposta.FirstOrDefault();
                }
            }
            catch (MySqlException e)
            {
                throw new DbException("Erro ao executar comando sql: " + e.ToString());
            }
        }

        public Login Post(Login usuario)
        {
            try
            {
                using (var cn = Conexao.AbrirConexao())
                {
                    cn.Execute(@"INSERT INTO T_LOGIN (USUARIO, SENHA) VALUES(@USUARIO, @SENHA)", new
                    {
                        usuario.USUARIO,
                        SENHA = usuario.retornarSenha()
                    }) ;
                }

                return Get(usuario.USUARIO, usuario.retornarSenha());
            }
            catch (MySqlException e)
            {
                throw new DbException("Erro ao executar comando sql: " + e.ToString());
            }

        }
    }
}